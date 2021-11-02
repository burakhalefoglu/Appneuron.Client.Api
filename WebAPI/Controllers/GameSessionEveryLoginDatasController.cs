using Business.Handlers.GameSessionEveryLoginDatas.Commands;
using Business.Handlers.GameSessionEveryLoginDatas.Queries;
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
    /// GameSessionEveryLoginDatas If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GameSessionEveryLoginDatasController : BaseApiController
    {
        ///<summary>
        ///List GameSessionEveryLoginDatas
        ///</summary>
        ///<remarks>GameSessionEveryLoginDatas</remarks>
        ///<return>List GameSessionEveryLoginDatas</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<IEnumerable<GameSessionEveryLoginData>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
        [HttpGet("getByProjectId")]
        public async Task<IActionResult> GetByProjectId(string ProjectId)
        {
            var result = await Mediator.Send(new GetGameSessionEveryLoginDatasByProjectIdQuery
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
        ///List GameSessionEveryLoginDatas
        ///</summary>
        ///<remarks>GameSessionEveryLoginDatas</remarks>
        ///<return>List GameSessionEveryLoginDatas</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<IEnumerable<RetentionDataWithSessionDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
        [HttpGet("getRetentionDataByProjectId")]
        public async Task<IActionResult> GetRetentionDataDtoByProjectId(string ProjectId)
        {
            var result = await Mediator.Send(new GetRetentionDataDtoByProjectIdQuery
            {
                ProjectID = ProjectId
            });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Delete GameSessionEveryLoginData.
        /// </summary>
        /// <param name="deleteGameSessionEveryLoginData"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
        [HttpDelete("deleteByProjectId")]
        public async Task<IActionResult> DeleteByProjectId([FromBody] DeleteGameSessionEveryLoginDataByProjectIdCommand deleteGameSessionEveryLoginData)
        {
            var result = await Mediator.Send(deleteGameSessionEveryLoginData);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}