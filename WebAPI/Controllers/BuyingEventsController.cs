using Business.Handlers.BuyingEvents.Commands;
using Business.Handlers.BuyingEvents.Queries;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    /// <summary>
    /// BuyingEvents If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BuyingEventsController : BaseApiController
    {
        ///<summary>
        ///List BuyingEvents
        ///</summary>
        ///<remarks>BuyingEvents</remarks>
        ///<return>List BuyingEvents</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BuyingEvent>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getByProjectId")]
        public async Task<IActionResult> GetByProjectId(string ProjectId)
        {
            var result = await Mediator.Send(new GetBuyingEventsByProjectIdQuery { 
            
                ProjectID = ProjectId
            });
            
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///List BuyingEvents
        ///</summary>
        ///<remarks>BuyingEvents</remarks>
        ///<return>List BuyingEvents</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BuyingEventDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getBuyingEventdtoByProjectId")]
        public async Task<IActionResult> GetBuyingEventdtoByProjectId(string ProjectId)
        {
            var result = await Mediator.Send(new GetBuyingEventdtoByProjectIdQuery
            {

                ProjectId = ProjectId
            });

            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete BuyingEvent.
        /// </summary>
        /// <param name="deleteBuyingEvent"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete("deleteByProjectId")]
        public async Task<IActionResult> DeleteByProjectId([FromBody] DeleteBuyingEventByProjectIdCommand deleteBuyingEvent)
        {
            var result = await Mediator.Send(deleteBuyingEvent);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}