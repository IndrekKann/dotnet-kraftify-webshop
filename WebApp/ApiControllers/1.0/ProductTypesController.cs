using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;
using V1DTO=PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Product types
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ProductTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ProductTypeMapper _mapper = new ProductTypeMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public ProductTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get the predefined ProductType collection.
        /// </summary>
        /// <returns>List of available ProductTypes</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.ProductType>))]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetProductTypes()
        {
            return Ok((await _bll.ProductTypes.GetAllAsync()).Select(e => _mapper.Map(e)).OrderBy(a => a.Name));
        }

        /// <summary>
        /// Get single ProductType
        /// </summary>
        /// <param name="id">ProductType Id</param>
        /// <returns>request ProductType</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ProductType))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<ProductType>> GetProductType(Guid id)
        {
            var productType = await _bll.ProductTypes.FirstOrDefaultAsync(id);
            
            if (productType == null)
            {
                return NotFound(new V1DTO.MessageDTO("ProductType not found"));
            }
            
            return Ok(_mapper.Map(productType));
        }

        /// <summary>
        /// Update the ProductType
        /// </summary>
        /// <param name="id">ProductType id</param>
        /// <param name="productType">ProductType object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutProductType(Guid id, ProductType productType)
        {
            if (id != productType.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and productType.id do not match"));
            }

            await _bll.ProductTypes.UpdateAsync(_mapper.Map(productType));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create a new ProductType
        /// </summary>
        /// <param name="productType">ProductType object</param>
        /// <returns>created ProductType object</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.ProductType))]
        public async Task<ActionResult<ProductType>> PostProductType(ProductType productType)
        {
            var bllEntity = _mapper.Map(productType);
            _bll.ProductTypes.Add(bllEntity);
            await _bll.SaveChangesAsync();
            productType.Id = bllEntity.Id;

            return CreatedAtAction(
                "GetProductType",
                new {id = productType.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                productType
                );
        }

        /// <summary>
        /// Delete the ProductType
        /// </summary>
        /// <param name="id">ProductType Id</param>
        /// <returns>deleted ProductType object</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ProductType))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<ProductType>> DeleteProductType(Guid id)
        {
            var productType = await _bll.ProductTypes.FirstOrDefaultAsync(id);
            if (productType == null)
            {
                return NotFound(new V1DTO.MessageDTO("ProductType not found"));
            }

            await _bll.ProductTypes.RemoveAsync(productType);
            await _bll.SaveChangesAsync();

            return Ok(productType);

        }

    }
}
