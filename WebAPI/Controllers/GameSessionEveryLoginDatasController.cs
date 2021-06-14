
using Business.Handlers.GameSessionEveryLoginDatas.Commands;
using Business.Handlers.GameSessionEveryLoginDatas.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;
using MongoDB.Bson;
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
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetGameSessionEveryLoginDatasQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>GameSessionEveryLoginDatas</remarks>
        ///<return>GameSessionEveryLoginDatas List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GameSessionEveryLoginData))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(string objectId)
        {
            var result = await Mediator.Send(new GetGameSessionEveryLoginDataQuery { ObjectId = objectId });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add GameSessionEveryLoginData.
        /// </summary>
        /// <param name="createGameSessionEveryLoginData"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateGameSessionEveryLoginDataCommand createGameSessionEveryLoginData)
        {
            var result = await Mediator.Send(createGameSessionEveryLoginData);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update GameSessionEveryLoginData.
        /// </summary>
        /// <param name="updateGameSessionEveryLoginData"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateGameSessionEveryLoginDataCommand updateGameSessionEveryLoginData)
        {
            var result = await Mediator.Send(updateGameSessionEveryLoginData);
            if (result.Success)
            {
                return Ok(result.Message);
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
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteGameSessionEveryLoginDataCommand deleteGameSessionEveryLoginData)
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
