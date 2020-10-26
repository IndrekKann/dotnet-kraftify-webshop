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
    /// Destinations
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class DestinationsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly DestinationMapper _mapper = new DestinationMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public DestinationsController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get the predefined Destinations collection.
        /// </summary>
        /// <returns>List of available Destinations</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.PaymentType>))]
        public async Task<ActionResult<IEnumerable<V1DTO.PaymentType>>> GetPaymentTypes()
        {
            return Ok((await _bll.Destinations.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get single Destination
        /// </summary>
        /// <param name="id">Destination Id</param>
        /// <returns>request Destination</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.PaymentType))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.PaymentType>> GetPaymentType(Guid id)
        {
            var destination = await _bll.Destinations.FirstOrDefaultAsync(id);

            if (destination == null)
            {
                return NotFound(new V1DTO.MessageDTO("Destination not found"));
            }

            return Ok(_mapper.Map(destination));
        }

        /// <summary>
        /// Update the Destination
        /// </summary>
        /// <param name="id">Destination id</param>
        /// <param name="destination">Destination object</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutPaymentType(Guid id, V1DTO.PaymentType destination)
        {
            if (id != destination.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and paymentType.id do not match"));
            }

            await _bll.Destinations.UpdateAsync(_mapper.Map(destination));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create a new Destination
        /// </summary>
        /// <param name="destination">PaymentType object</param>
        /// <returns>created PaymentType object</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.PaymentType))]
        public async Task<ActionResult<V1DTO.PaymentType>> PostPaymentType(V1DTO.PaymentType destination)
        {
            var bllEntity = _mapper.Map(destination);
            _bll.Destinations.Add(bllEntity);
            await _bll.SaveChangesAsync();
            destination.Id = bllEntity.Id;

            return CreatedAtAction(
                "GetPaymentType",
                new {id = destination.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                destination
                );
        }

        /// <summary>
        /// Delete the Destination
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
            var destination = await _bll.Destinations.FirstOrDefaultAsync(id);
            
            if (destination == null)
            {
                return NotFound(new V1DTO.MessageDTO("Destination not found"));
            }

            await _bll.Destinations.RemoveAsync(destination);
            await _bll.SaveChangesAsync();

            return Ok(destination);
        }

    }
}
