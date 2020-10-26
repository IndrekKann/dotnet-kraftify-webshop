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
    /// Currencies Api Controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class CurrenciesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly CurrencyMapper _mapper = new CurrencyMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public CurrenciesController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get the list of all the Currencies
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.Currency>))]
        public async Task<ActionResult<IEnumerable<V1DTO.Currency>>> GetCurrencies()
        {
            return Ok((await _bll.Currencies.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get single Currency
        /// </summary>
        /// <param name="id">Guid id of item to get</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Currency))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.Currency>> GetCurrency(Guid id)
        {
            var currency = await _bll.Currencies.FirstOrDefaultAsync(id);

            if (currency == null)
            {
                return NotFound(new V1DTO.MessageDTO("Currency not found"));
            }

            return Ok(_mapper.Map(currency));
        }

        /// <summary>
        /// update Currency
        /// </summary>
        /// <param name="id">Item id</param>
        /// <param name="currency">Currency</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutCurrency(Guid id, V1DTO.Currency currency)
        {
            if (id != currency.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and currency.id do not match"));
            }

            await _bll.Currencies.UpdateAsync(_mapper.Map(currency));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create new Currency 
        /// </summary>
        /// <param name="currency">Currency to create</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Currency))]
        public async Task<ActionResult<V1DTO.Currency>> PostCurrency(V1DTO.Currency currency)
        {
            var bllEntity = _mapper.Map(currency);
            _bll.Currencies.Add(bllEntity);
            await _bll.SaveChangesAsync();
            currency.Id = bllEntity.Id;

            return CreatedAtAction(
                "GetCurrency",
                new {id = currency.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                currency
                );
        }

        /// <summary>
        /// Delete Currency
        /// </summary>
        /// <param name="id">Guid id of item to delete</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Currency))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.Currency>> DeleteCurrency(Guid id)
        {
            var currency = await _bll.Currencies.FirstOrDefaultAsync(id);
            
            if (currency == null)
            {
                return NotFound(new V1DTO.MessageDTO("Currencies not found"));
            }

            await _bll.Currencies.RemoveAsync(currency);
            await _bll.SaveChangesAsync();

            return Ok(currency);
        }

    }
}
