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
    /// Payments Api Controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class PaymentsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PaymentMapper _mapper = new PaymentMapper();
        private readonly OrderMapper _orderMapper = new OrderMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public PaymentsController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get the list of all the Payments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.Payment>))]
        public async Task<ActionResult<IEnumerable<V1DTO.Payment>>> GetPayments()
        {
            return Ok((await _bll.Payments.GetAllForViewAsync()).OrderByDescending(a => int.Parse(a.OrderNumber)).Select(e => _mapper.MapPaymentView(e)));
        }

        /// <summary>
        /// Get single Payment
        /// </summary>
        /// <param name="id">Guid id of item to get</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Payment))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.Payment>> GetPayment(Guid id)
        {
            var payment = await _bll.Payments.FirstOrDefaultForViewAsync(id);

            if (payment == null)
            {
                return NotFound(new V1DTO.MessageDTO("Payment not found"));
            }

            return Ok(_mapper.MapPaymentView(payment));
        }

        /// <summary>
        /// update Payment
        /// </summary>
        /// <param name="id">Item id</param>
        /// <param name="payment">Payment</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutPayment(Guid id, V1DTO.Payment payment)
        {
            if (id != payment.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and payment.id do not match"));
            }

            await _bll.Payments.UpdateAsync(_mapper.Map(payment));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create new Payment 
        /// </summary>
        /// <param name="paymentCreate">Payment to create</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.PaymentCreate))]
        public async Task<ActionResult<V1DTO.PaymentCreate>> PostPayment(V1DTO.PaymentCreate paymentCreate)
        {
            var orderId = _bll.Orders.PlaceOrderForApi(paymentCreate).Result;
            await _bll.Payments.MakePaymentForApi(paymentCreate, orderId);
            await _bll.Orders.CopyShoppingCart(paymentCreate.ShoppingCartId, orderId);
            await _bll.ShoppingCarts.ClearShoppingCart(paymentCreate.ShoppingCartId);
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }

        /// <summary>
        /// Delete Payment
        /// </summary>
        /// <param name="id">Guid id of item to delete</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Payment))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.Payment>> DeletePayment(Guid id)
        {
            var payment = await _bll.Payments.FirstOrDefaultAsync(id);
            
            if (payment == null)
            {
                return NotFound(new V1DTO.MessageDTO("Payment not found"));
            }

            await _bll.Payments.RemoveAsync(payment);
            await _bll.SaveChangesAsync();

            return Ok(payment);
        }

    }
}
