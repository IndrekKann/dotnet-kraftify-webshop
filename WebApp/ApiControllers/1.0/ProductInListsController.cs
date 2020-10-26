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
    /// ProductInLists Api Controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ProductInListsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ProductInListMapper _mapper = new ProductInListMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public ProductInListsController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get the list of all the ProductInLists
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.ProductInList>))]
        public async Task<ActionResult<IEnumerable<V1DTO.ProductInList>>> GetProductInLists()
        {
            return Ok((await _bll.ProductInLists.GetAllForViewAsync()).Select(e => _mapper.MapProductInListView(e)));
        }

        /// <summary>
        /// Get single ProductInList
        /// </summary>
        /// <param name="scId">Shopping cart id to get all the items in that cart</param>
        /// <returns></returns>
        [HttpGet("{scId}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ProductInList))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.ProductInList>> GetProductInList(Guid scId)
        {
            if (scId == null)
            {
                return NotFound(new V1DTO.MessageDTO("ProductInList not found"));
            }

            return Ok((await _bll.ProductInLists.GetProductsForShoppingCartViewAsync(scId)).Select(e => _mapper.MapProductInListView(e)));
        }

        /// <summary>
        /// update ProductInList
        /// </summary>
        /// <param name="id">Item id</param>
        /// <param name="productInList">ProductInList</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutProductInList(Guid id, V1DTO.ProductInList productInList)
        {
            if (id != productInList.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and productInList.id do not match"));
            }

            await _bll.ProductInLists.DecreaseQuantity(_mapper.Map(productInList));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create new ProductInList, adding a product to shopping cart
        /// </summary>
        /// <param name="productInList">ProductInList to create</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.ProductInList))]
        public async Task<ActionResult<V1DTO.ProductInList>> PostProductInList(V1DTO.ProductInList productInList)
        {
            await _bll.ProductInLists.AddToShoppingCartApi(productInList.ProductId, productInList.ShoppingCartId);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete ProductInList, removing a product from shopping cart
        /// </summary>
        /// <param name="id">Guid id of item to delete</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ProductInList))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.ProductInList>> DeleteProductInList(Guid id)
        {
            var productInList = await _bll.ProductInLists.FirstOrDefaultAsync(id);
            
            if (productInList == null)
            {
                return NotFound(new V1DTO.MessageDTO("ProductInList not found"));
            }

            await _bll.ProductInLists.RemoveAsync(productInList);
            await _bll.SaveChangesAsync();

            return Ok(productInList);
        }

    }
}
