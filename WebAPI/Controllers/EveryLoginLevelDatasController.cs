using Business.Handlers.EveryLoginLevelDatas.Commands;
using Business.Handlers.EveryLoginLevelDatas.Queries;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<IEnumerable<EveryLoginLevelData>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
        [HttpGet("getByProjectId")]
        public async Task<IActionResult> GetByProjectId(long projectId)
        {
            var result = await Mediator.Send(new GetEveryLoginLevelDatasByProjectIdQuery { 
            
                ProjectId = projectId

            });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        ///<summary>
        ///List EveryLoginLevelDatas
        ///</summary>
        ///<remarks>EveryLoginLevelDatas</remarks>
        ///<return>List EveryLoginLevelDatas</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<IEnumerable<TotalPowerUsageDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
        [HttpGet("getTotalPowerUsageDtoByProjectId")]
        public async Task<IActionResult> GetTotalPowerUsageDtoByProjectId(long projectId)
        {
            var result = await Mediator.Send(new GetTotalPowerUsageDtoByProjectIdQuery
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
        ///List EveryLoginLevelDatas
        ///</summary>
        ///<remarks>EveryLoginLevelDatas</remarks>
        ///<return>List EveryLoginLevelDatas</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<IEnumerable<LevelbasePowerUsageDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
        [HttpGet("getLevelBasePowerUsageDtoByProjectIdQuery")]
        public async Task<IActionResult> GetLevelBasePowerUsageDtoByProjectId(long projectId)
        {
            var result = await Mediator.Send(new GetLevelbasePowerUsageDtoByProjectIdQuery
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
        ///List EveryLoginLevelDatas
        ///</summary>
        ///<remarks>EveryLoginLevelDatas</remarks>
        ///<return>List EveryLoginLevelDatas</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<IEnumerable<LevelbaseFinishScoreDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
        [HttpGet("getLevelBaseFinishScoreByProjectIdQuery")]
        public async Task<IActionResult> GetLevelBaseFinishScoreByProjectId(long projectId)
        {
            var result = await Mediator.Send(new GetLevelbaseFinishScoreByProjectIdQuery
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
        /// Delete EveryLoginLevelData.
        /// </summary>
        /// <param name="deleteEveryLoginLevelData"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
        [HttpDelete("deleteByProjectId")]
        public async Task<IActionResult> DeleteByProjectId([FromBody] DeleteEveryLoginLevelDataByProjectIdCommand deleteEveryLoginLevelData)
        {
            var result = await Mediator.Send(deleteEveryLoginLevelData);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}