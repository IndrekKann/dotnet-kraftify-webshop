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
    /// PaymentTypes
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class PaymentTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PaymentTypeMapper _mapper = new PaymentTypeMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public PaymentTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get the predefined PaymentType collection.
        /// </summary>
        /// <returns>List of available PaymentTypes</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.PaymentType>))]
        public async Task<ActionResult<IEnumerable<V1DTO.PaymentType>>> GetPaymentTypes()
        {
            return Ok((await _bll.PaymentTypes.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get single PaymentType
        /// </summary>
        /// <param name="id">PaymentType Id</param>
        /// <returns>request PaymentType</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.PaymentType))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.PaymentType>> GetPaymentType(Guid id)
        {
            var paymentType = await _bll.PaymentTypes.FirstOrDefaultAsync(id);

            if (paymentType == null)
            {
                return NotFound(new V1DTO.MessageDTO("PaymentType not found"));
            }

            return Ok(_mapper.Map(paymentType));
        }

        /// <summary>
        /// Update the PaymentType
        /// </summary>
        /// <param name="id">PaymentType id</param>
        /// <param name="paymentType">PaymentType object</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutPaymentType(Guid id, V1DTO.PaymentType paymentType)
        {
            if (id != paymentType.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and paymentType.id do not match"));
            }

            await _bll.PaymentTypes.UpdateAsync(_mapper.Map(paymentType));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create a new PaymentType
        /// </summary>
        /// <param name="paymentType">PaymentType object</param>
        /// <returns>created PaymentType object</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.PaymentType))]
        public async Task<ActionResult<V1DTO.PaymentType>> PostPaymentType(V1DTO.PaymentType paymentType)
        {
            var bllEntity = _mapper.Map(paymentType);
            _bll.PaymentTypes.Add(bllEntity);
            await _bll.SaveChangesAsync();
            paymentType.Id = bllEntity.Id;

            return CreatedAtAction(
                "GetPaymentType",
                new {id = paymentType.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                paymentType
                );
        }

        /// <summary>
        /// Delete the PaymentType
        /// </summary>
        /// <param name="id">PaymentType Id</param>
        /// <returns>deleted PaymentType object</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.PaymentType))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.PaymentType>> DeletePaymentType(Guid id)
        {
            var paymentType = await _bll.PaymentTypes.FirstOrDefaultAsync(id);
            
            if (paymentType == null)
            {
                return NotFound(new V1DTO.MessageDTO("PaymentType not found"));
            }

            await _bll.PaymentTypes.RemoveAsync(paymentType);
            await _bll.SaveChangesAsync();

            return Ok(paymentType);
        }

    }
}
