using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1.Mappers;
using V1DTO=PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Products
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ProductsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ProductMapper _mapper = new ProductMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public ProductsController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get all the products
        /// </summary>
        /// <returns>Array of Products</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.ProductView>))]
        public async Task<ActionResult<IEnumerable<V1DTO.ProductView>>> GetProducts(string? search = "", string? searchType = "", string? order = "", string? limit = "18", string? page = "1")
        {
            var result = await _bll.Products.GetAllForViewAsync(search, searchType, order, limit, page);
            return Ok(result.Select(e => _mapper.MapProductView(e)));
        }

        /// <summary>
        /// Get a single Product
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>Product object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ProductView))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.ProductView>> GetProduct(Guid id)
        {
            var product = await _bll.Products.FirstOrDefaultViewAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(_mapper.MapProductView(product));
        }

        /// <summary>
        /// Update the Product
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <param name="product">Product object</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutProduct(Guid id, V1DTO.Product product)
        {
            if (id != product.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("Id and gpsSession.id do not match"));
            }

            if (!await _bll.Products.ExistsAsync(product.Id, User.UserGuidId()))
            {
                return NotFound(new V1DTO.MessageDTO($"Current user does not have session with this id {id}."));
            }

            await _bll.Products.UpdateAsync(_mapper.Map(product));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Post the new Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Product))]
        public async Task<ActionResult<V1DTO.Product>> PostProduct(V1DTO.Product product)
        {
            var bllEntity = _mapper.Map(product);
            _bll.Products.Add(bllEntity);
            await _bll.SaveChangesAsync();
            product.Id = bllEntity.Id;

            return CreatedAtAction(
                "GetProduct",
                new {id = product.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"}, 
                product
                );
        }

        /// <summary>
        /// Delete the Product.
        /// </summary>
        /// <param name="id">Product Id to delete.</param>
        /// <returns>Product just deleted</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Product))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.Product>> DeleteProduct(Guid id)
        {
            var userIdTKey = User.IsInRole("admin") ? null : (Guid?) User.UserGuidId();

            var product = await _bll.Products.FirstOrDefaultAsync(id, userIdTKey);

            if (product == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Product with id {id} is not found!"));
            }
            await _bll.Products.RemoveAsync(product, userIdTKey);
            await _bll.SaveChangesAsync();

            return Ok(product);
        }

    }
}
