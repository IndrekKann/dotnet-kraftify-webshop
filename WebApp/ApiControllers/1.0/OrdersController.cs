#pragma warning disable 1998
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
    /// Orders Api Controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class OrdersController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly OrderMapper _mapper = new OrderMapper();
        private readonly ProductInListMapper _orderMapper = new ProductInListMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public OrdersController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get the list of all the Orders
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.Order>))]
        public async Task<ActionResult<V1DTO.Order>> GetOrders(Guid appUserId)
        {
            return Ok((await _bll.Orders.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get products for Order
        /// </summary>
        /// <param name="id">Payment Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ProductInList))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.ProductInList>> GetOrder(Guid id)
        {
            var payment = await _bll.Payments.FirstOrDefaultAsync(id);

            if (payment == null)
            {
                return NotFound(new V1DTO.MessageDTO("ProductInList not found"));
            }

            return Ok((await _bll.ProductInLists.GetProductsForOrderViewAsync(payment.OrderId)).Select(e => _orderMapper.MapProductInListView(e)));
        }

        /// <summary>
        /// update Order
        /// </summary>
        /// <param name="id">Item id</param>
        /// <param name="order">Order</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutOrder(Guid id, V1DTO.Order order)
        {
            if (id != order.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and order.id do not match"));
            }

            await _bll.Orders.UpdateAsync(_mapper.Map(order));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create new Order 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Order))]
        public async Task<ActionResult<V1DTO.Order>> PostOrder(V1DTO.OrderCreateDTO order)
        {
            if (order.ShoppingCartId != null)
            {
                // This orderId should be used to redirect user to Payments page
                var orderId = await _bll.Orders.PlaceOrder(order.ShoppingCartId, order.AppUserId);
                await _bll.SaveChangesAsync();
            }
            
            
            return CreatedAtAction("GetOrder", new {id = order, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"});
        }

        /// <summary>
        /// Delete Order
        /// </summary>
        /// <param name="id">Guid id of item to delete</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Order))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.Order>> DeleteOrder(Guid id)
        {
            var order = await _bll.Orders.FirstOrDefaultAsync(id);
            
            if (order == null)
            {
                return NotFound(new V1DTO.MessageDTO("Order not found"));
            }

            await _bll.Orders.RemoveAsync(order);
            await _bll.SaveChangesAsync();

            return Ok(order);
        }

    }
}
