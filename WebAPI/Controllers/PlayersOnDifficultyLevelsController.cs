
using Business.Handlers.PlayersOnDifficultyLevels.Commands;
using Business.Handlers.PlayersOnDifficultyLevels.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;

namespace WebAPI.Controllers
{
    /// <summary>
    /// PlayersOnDifficultyLevels If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersOnDifficultyLevelsController : BaseApiController
    {
        ///<summary>
        ///List PlayersOnDifficultyLevels
        ///</summary>
        ///<remarks>PlayersOnDifficultyLevels</remarks>
        ///<return>List PlayersOnDifficultyLevels</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProjectBasePlayerCountOnDifficultyLevel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetPlayersOnDifficultyLevelsQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>PlayersOnDifficultyLevels</remarks>
        ///<return>PlayersOnDifficultyLevels List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectBasePlayerCountOnDifficultyLevel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(string objectId)
        {
            var result = await Mediator.Send(new GetPlayersOnDifficultyLevelQuery { ObjectId = objectId });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add PlayersOnDifficultyLevel.
        /// </summary>
        /// <param name="createPlayersOnDifficultyLevel"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreatePlayersOnDifficultyLevelCommand createPlayersOnDifficultyLevel)
        {
            var result = await Mediator.Send(createPlayersOnDifficultyLevel);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update PlayersOnDifficultyLevel.
        /// </summary>
        /// <param name="updatePlayersOnDifficultyLevel"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdatePlayersOnDifficultyLevelCommand updatePlayersOnDifficultyLevel)
        {
            var result = await Mediator.Send(updatePlayersOnDifficultyLevel);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete PlayersOnDifficultyLevel.
        /// </summary>
        /// <param name="deletePlayersOnDifficultyLevel"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeletePlayersOnDifficultyLevelCommand deletePlayersOnDifficultyLevel)
        {
            var result = await Mediator.Send(deletePlayersOnDifficultyLevel);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
