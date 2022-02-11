using Business.Handlers.AdvEvents.Commands;
using Business.Handlers.AdvEvents.Queries;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Utilities.Results;

namespace WebAPI.Controllers
{
    /// <summary>
    /// AdvEvents If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AdvEventsController : BaseApiController
    {
        ///<summary>
        ///List AdvEvents
        ///</summary>
        ///<remarks>AdvEvents</remarks>
        ///<return>List AdvEvents</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<IEnumerable<AdvEvent>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
        [HttpGet("getByProjectId")]
        public async Task<IActionResult> GetByProjectId(long projectId)
        {
            var result = await Mediator.Send(new GetAdvEventsByProjectIdQuery { 
            
                ProjectId = projectId
            });

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        ///<summary>
        ///List AdvEvents
        ///</summary>
        ///<remarks>AdvEvents</remarks>
        ///<return>List AdvEvents</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<IEnumerable<ClickbaseAdvEventDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
        [HttpGet("getClickBaseAdvEventByProjectId")]
        public async Task<IActionResult> GetClickBaseAdvEventDtoListQuery(long projectId)
        {
            var result = await Mediator.Send(new GetClickBaseAdvEventDtoListQuery
            {
                ProjectId = projectId
            });

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        

        /// <summary>
        /// Delete AdvEvent.
        /// </summary>
        /// <param name="deleteAdvEvent"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
        [HttpDelete("deleteByProjectId")]
        public async Task<IActionResult> DeleteByProjectId([FromBody] DeleteAdvEventByProjectIdCommand deleteAdvEvent)
        {
            var result = await Mediator.Send(deleteAdvEvent);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}