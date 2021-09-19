using Business.Handlers.EveryLoginLevelDatas.Commands;
using Business.Handlers.EveryLoginLevelDatas.Queries;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    /// <summary>
    /// EveryLoginLevelDatas If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EveryLoginLevelDatasController : BaseApiController
    {
        ///<summary>
        ///List EveryLoginLevelDatas
        ///</summary>
        ///<remarks>EveryLoginLevelDatas</remarks>
        ///<return>List EveryLoginLevelDatas</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EveryLoginLevelData>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getByProjectId")]
        public async Task<IActionResult> GetByProjectId(string ProjectId)
        {
            var result = await Mediator.Send(new GetEveryLoginLevelDatasByProjectIdQuery { 
            
                ProjectID = ProjectId

            });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///List EveryLoginLevelDatas
        ///</summary>
        ///<remarks>EveryLoginLevelDatas</remarks>
        ///<return>List EveryLoginLevelDatas</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TotalPowerUsageDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getTotalPowerUsageDtoByProjectId")]
        public async Task<IActionResult> GetTotalPowerUsageDtoByProjectId(string ProjectId)
        {
            var result = await Mediator.Send(new GetTotalPowerUsageDtoByProjectIdQuery
            {
                ProjectId = ProjectId
            });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }


        ///<summary>
        ///List EveryLoginLevelDatas
        ///</summary>
        ///<remarks>EveryLoginLevelDatas</remarks>
        ///<return>List EveryLoginLevelDatas</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LevelbasePowerUsageDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getLevelbasePowerUsageDtoByProjectIdQuery")]
        public async Task<IActionResult> GetLevelbasePowerUsageDtoByProjectId(string ProjectId)
        {
            var result = await Mediator.Send(new GetLevelbasePowerUsageDtoByProjectIdQuery
            {
                ProjectId = ProjectId
            });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }


        ///<summary>
        ///List EveryLoginLevelDatas
        ///</summary>
        ///<remarks>EveryLoginLevelDatas</remarks>
        ///<return>List EveryLoginLevelDatas</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LevelbaseFinishScoreDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getLevelbaseFinishScoreByProjectIdQuery")]
        public async Task<IActionResult> GetLevelbaseFinishScoreByProjectId(string ProjectId)
        {
            var result = await Mediator.Send(new GetLevelbaseFinishScoreByProjectIdQuery
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
        /// Delete EveryLoginLevelData.
        /// </summary>
        /// <param name="deleteEveryLoginLevelData"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete("deleteByProjectId")]
        public async Task<IActionResult> DeleteByProjectId([FromBody] DeleteEveryLoginLevelDataByProjectIdCommand deleteEveryLoginLevelData)
        {
            var result = await Mediator.Send(deleteEveryLoginLevelData);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}