using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1.Mappers;
using V1DTO=PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Prices Api Controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class PricesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PriceMapper _mapper = new PriceMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public PricesController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        /// <summary>
        /// Get the list of all the Prices
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.Price>))]
        public async Task<ActionResult<IEnumerable<V1DTO.Price>>> GetPrices()
        {
            return Ok((await _bll.Prices.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get single Price
        /// </summary>
        /// <param name="id">Guid id of item to get</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Price))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.Price>> GetPrice(Guid id)
        {
            var price = await _bll.Prices.FirstOrDefaultAsync(id);

            if (price == null)
            {
                return NotFound(new V1DTO.MessageDTO("Price not found"));
            }

            return Ok(_mapper.Map(price));
        }

        /// <summary>
        /// update Price
        /// </summary>
        /// <param name="id">Item id</param>
        /// <param name="price">GpsSessionType</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutPrice(Guid id, V1DTO.Price price)
        {
            if (id != price.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and price.id do not match"));
            }

            await _bll.Prices.UpdateAsync(_mapper.Map(price));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create new Price 
        /// </summary>
        /// <param name="price">Price to create</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Price))]
        public async Task<ActionResult<V1DTO.Price>> PostPrice(V1DTO.Price price)
        {
            var bllEntity = _mapper.Map(price);
            _bll.Prices.Add(bllEntity);
            await _bll.SaveChangesAsync();
            price.Id = bllEntity.Id;

            return CreatedAtAction(
                "GetPrice",
                new {id = price.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                price
                );
        }

        /// <summary>
        /// Delete Price
        /// </summary>
        /// <param name="id">Guid id of item to delete</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Price))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.Price>> DeletePrice(Guid id)
        {
            var price = await _bll.Prices.FirstOrDefaultAsync(id);
            
            if (price == null)
            {
                return NotFound(new V1DTO.MessageDTO("Price not found"));
            }

            await _bll.Prices.RemoveAsync(price);
            await _bll.SaveChangesAsync();

            return Ok(price);
        }

    }
}
