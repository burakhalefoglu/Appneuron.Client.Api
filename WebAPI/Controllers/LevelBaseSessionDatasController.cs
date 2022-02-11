using Business.Handlers.LevelBaseSessionDatas.Commands;
using Business.Handlers.LevelBaseSessionDatas.Queries;
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
    /// LevelBaseSessionDatas If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LevelBaseSessionDatasController : BaseApiController
    {
        ///<summary>
        ///List LevelBaseSessionDatas
        ///</summary>
        ///<remarks>LevelBaseSessionDatas</remarks>
        ///<return>List LevelBaseSessionDatas</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<IEnumerable<LevelBaseSessionData>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
        [HttpGet("getByProjectId")]
        public async Task<IActionResult> GetByProjectId(long projectId)
        {
            var result = await Mediator.Send(new GetLevelBaseSessionDatasByProjectIdQuery
            {
                ProjectId = projectId
            });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        ///<summary>
        ///List LevelBaseSessionDatas
        ///</summary>
        ///<remarks>LevelBaseSessionDatas</remarks>
        ///<return>List LevelBaseSessionDatas</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<IEnumerable<LevelbaseSessionWithPlayingTimeDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
        [HttpGet("getLevelBaseSessionWithPlayingTimeDtoByProjectId")]
        public async Task<IActionResult> GetLevelBaseSessionWithPlayingTimeDtoByProjectId(long projectId)
        {
            var result = await Mediator.Send(new GetLevelbaseSessionWithPlayingTimeDtoByProjectIdQuery
            {
                ProjectId = projectId
            });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        ///<summary>
        ///List LevelBaseSessionDatas
        ///</summary>
        ///<remarks>LevelBaseSessionDatas</remarks>
        ///<return>List LevelBaseSessionDatas</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<IEnumerable<LevelbaseSessionDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
        [HttpGet("getSessionDtoByProjectId")]
        public async Task<IActionResult> GetSessionDtoByProjectId(long projectId)
        {
            var result = await Mediator.Send(new GetLevelBaseSessionDtoDatasByProjectIdQuery
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
        /// Delete LevelBaseSessionData.
        /// </summary>
        /// <param name="deleteLevelBaseSessionData"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
        [HttpDelete("deleteByProjectId")]
        public async Task<IActionResult> DeleteByProjectId([FromBody] DeleteLevelBaseSessionDataByProjectIdCommand deleteLevelBaseSessionData)
        {
            var result = await Mediator.Send(deleteLevelBaseSessionData);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}