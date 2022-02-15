using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Handlers.EnemyBaseLevelFailModels.Commands;
using Business.Handlers.EnemyBaseLevelFailModels.Queries;
using Business.Handlers.LevelBaseDieDatas.Queries;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    ///     LevelBaseDieDatas If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LevelBaseDieDatasController : BaseApiController
    {
        /// <summary>
        ///     List LevelBaseDieDatas
        /// </summary>
        /// <remarks>LevelBaseDieDatas</remarks>
        /// <return>List LevelBaseDieDatas</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<IEnumerable<EnemyBaseLevelFailModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
        [HttpGet("getByProjectId")]
        public async Task<IActionResult> GetByProjectId(long projectId)
        {
            var result = await Mediator.Send(new GetEnemyBaseLevelFailModelsByProjectIdQuery
            {
                ProjectId = projectId
            });
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        ///     List LevelBaseDieDatas
        /// </summary>
        /// <remarks>LevelBaseDieDatas</remarks>
        /// <return>List LevelBaseDieDatas</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<IEnumerable<LevelbaseFailDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
        [HttpGet("getLevelBaseFailDtoByProjectId")]
        public async Task<IActionResult> GetLevelBaseFailDtoByProjectId(long projectId)
        {
            var result = await Mediator.Send(new GetEnemyBaseLevelFailModelsDtoByProjectIdQuery
            {
                ProjectId = projectId
            });
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }


        /// <summary>
        ///     List LevelBaseDieDatas
        /// </summary>
        /// <remarks>LevelBaseDieDatas</remarks>
        /// <return>List LevelBaseDieDatas</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<IEnumerable<DailyDieCountDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
        [HttpGet("getDailyDieCountDto")]
        public async Task<IActionResult> GetDailyDieCountDto(long projectId)
        {
            var result = await Mediator.Send(new GetDailyDieCountDtoByProjectIdQuery
            {
                ProjectId = projectId
            });
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }


        /// <summary>
        ///     Delete LevelBaseDieData.
        /// </summary>
        /// <param name="deleteLevelBaseDieData"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
        [HttpDelete("deleteByProjectId")]
        public async Task<IActionResult> DeleteByProjectId(
            [FromBody] DeleteEnemyBaseLevelFailModelByProjectIdCommand deleteLevelBaseDieData)
        {
            var result = await Mediator.Send(deleteLevelBaseDieData);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
    }
}