﻿using Business.Handlers.AdvEvents.Commands;
using Business.Handlers.AdvEvents.Queries;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AdvEvent>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getByProjectId")]
        public async Task<IActionResult> GetByProjectId(string ProjectId)
        {
            var result = await Mediator.Send(new GetAdvEventsByProjectIdQuery { 
            
                ProjectID = ProjectId
            });

            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///List AdvEvents
        ///</summary>
        ///<remarks>AdvEvents</remarks>
        ///<return>List AdvEvents</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClickbaseAdvEventDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getClickbaseAdvEventByProjectId")]
        public async Task<IActionResult> getClickbaseAdvEventDtoListQuery(string ProjectId)
        {
            var result = await Mediator.Send(new GetClickbaseAdvEventDtoListQuery
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
        /// Delete AdvEvent.
        /// </summary>
        /// <param name="deleteAdvEvent"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete("deleteByProjectId")]
        public async Task<IActionResult> DeleteByProjectId([FromBody] DeleteAdvEventByProjectIdCommand deleteAdvEvent)
        {
            var result = await Mediator.Send(deleteAdvEvent);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

    }
}