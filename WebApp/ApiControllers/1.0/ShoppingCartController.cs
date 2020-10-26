#pragma warning restore 1998
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1.Mappers;
using V1DTO=PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// ShoppingCart
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ShoppingCartMapper _mapper = new ShoppingCartMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public ShoppingCartController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// get all the ShoppingCarts
        /// </summary>
        /// <returns>Array of ShoppingCarts</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.ShoppingCart>))]
        public async Task<ActionResult<IEnumerable<V1DTO.ShoppingCart>>> GetShoppingCarts()
        {
            return Ok((await _bll.ShoppingCarts.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get a single ShoppingCart by AppUserId
        /// </summary>
        /// <param name="id">ShoppingCart Id</param>
        /// <returns>ShoppingCart object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ShoppingCart))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
#pragma warning disable 1998
        public async Task<ActionResult<V1DTO.ShoppingCart>> GetShoppingCart(Guid id)
#pragma warning restore 1998
        {
            var shoppingCart = _bll.ShoppingCarts.GetByAppUserId(id);

            if (shoppingCart == null)
            {
                return NotFound(new V1DTO.MessageDTO($"shoppingCart with id {id} not found"));
            }

            return Ok(_mapper.Map(shoppingCart));
        }

        /// <summary>
        /// Update the ShoppingCart
        /// </summary>
        /// <param name="id">ShoppingCart Id</param>
        /// <param name="shoppingCart">ShoppingCart object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutShoppingCart(Guid id, V1DTO.ShoppingCart shoppingCart)
        {
            if (id != shoppingCart.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("Id and shoppingCart.id do not match"));
            }

            if (!await _bll.ShoppingCarts.ExistsAsync(shoppingCart.Id, User.UserGuidId()))
            {
                return NotFound(new V1DTO.MessageDTO($"Current user does not have shoppingCart with this id {id}"));
            }
            
            await _bll.ShoppingCarts.UpdateAsync(_mapper.Map(shoppingCart));
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }

        /// <summary>
        /// Post the new ShoppingCart
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.ShoppingCart))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.ShoppingCart>> PostShoppingCart(V1DTO.ShoppingCart shoppingCart)
        {
            var bllEntity = _mapper.Map(shoppingCart);
            _bll.ShoppingCarts.Add(bllEntity);
            await _bll.SaveChangesAsync();
            shoppingCart.Id = bllEntity.Id;

            return CreatedAtAction(
                "GetShoppingCart",
                new {id = shoppingCart.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                shoppingCart
                );
        }

        /// <summary>
        /// Delete the ShoppingCart.
        /// </summary>
        /// <param name="id">ShoppingCart Id to delete.</param>
        /// <returns>ShoppingCart just deleted</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ShoppingCart))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.ShoppingCart>> DeleteShoppingCart(Guid id)
        {
            var userIdTKey = User.IsInRole("admin") ? null : (Guid?) User.UserGuidId();

            var shoppingCart =
                await _bll.ShoppingCarts.FirstOrDefaultAsync(id, userIdTKey);

            
            if (shoppingCart == null)
            {
                return NotFound(new V1DTO.MessageDTO($"ShoppingCart with id {id} not found!"));
            }

            await _bll.ShoppingCarts.RemoveAsync(shoppingCart, userIdTKey);
            await _bll.SaveChangesAsync();

            return Ok(shoppingCart);
        }

    }
}
