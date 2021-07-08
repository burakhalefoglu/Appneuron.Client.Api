using Business.Handlers.GameSessionEveryLoginDatas.Commands;
using Business.Handlers.GameSessionEveryLoginDatas.Queries;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GameSessionEveryLoginData>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GameSessionEveryLoginData>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getDtoByProjectId")]
        public async Task<IActionResult> GetDtoByProjectId(string ProjectId)
        {
            var result = await Mediator.Send(new GetGameSessionEveryLoginDatasDtoByProjectIdQuery
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
        /// Delete GameSessionEveryLoginData.
        /// </summary>
        /// <param name="deleteGameSessionEveryLoginData"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete("deleteByProjectId")]
        public async Task<IActionResult> DeleteByProjectId([FromBody] DeleteGameSessionEveryLoginDataByProjectIdCommand deleteGameSessionEveryLoginData)
        {
            var result = await Mediator.Send(deleteGameSessionEveryLoginData);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}