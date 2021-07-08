using Business.Handlers.LevelBaseSessionDatas.Commands;
using Business.Handlers.LevelBaseSessionDatas.Queries;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LevelBaseSessionData>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getByProjectId")]
        public async Task<IActionResult> GetByProjectId(string ProjectId)
        {
            var result = await Mediator.Send(new GetLevelBaseSessionDatasByProjectIdQuery
            {
                ProjectID = ProjectId
            });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///List LevelBaseSessionDatas
        ///</summary>
        ///<remarks>LevelBaseSessionDatas</remarks>
        ///<return>List LevelBaseSessionDatas</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LevelBaseSessionData>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getDtoByProjectId")]
        public async Task<IActionResult> GetDtoByProjectId(string ProjectId)
        {
            var result = await Mediator.Send(new GetLevelBaseSessionDatasDtoByProjectIdQuery
            {
                ProjectID = ProjectId
            });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete LevelBaseSessionData.
        /// </summary>
        /// <param name="deleteLevelBaseSessionData"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete("deleteByProjectId")]
        public async Task<IActionResult> DeleteByProjectId([FromBody] DeleteLevelBaseSessionDataByProjectIdCommand deleteLevelBaseSessionData)
        {
            var result = await Mediator.Send(deleteLevelBaseSessionData);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}