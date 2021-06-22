
using Business.Handlers.ProjectBaseFinishingScoreWithLevels.Commands;
using Business.Handlers.ProjectBaseFinishingScoreWithLevels.Queries;
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
    /// ProjectBaseFinishingScoreWithLevels If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectBaseFinishingScoreWithLevelsController : BaseApiController
    {
        ///<summary>
        ///List ProjectBaseFinishingScoreWithLevels
        ///</summary>
        ///<remarks>ProjectBaseFinishingScoreWithLevels</remarks>
        ///<return>List ProjectBaseFinishingScoreWithLevels</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FinishingScoreWithLevel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetProjectBaseFinishingScoreWithLevelsQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>ProjectBaseFinishingScoreWithLevels</remarks>
        ///<return>ProjectBaseFinishingScoreWithLevels List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FinishingScoreWithLevel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(string objectId)
        {
            var result = await Mediator.Send(new GetProjectBaseFinishingScoreWithLevelQuery { ObjectId = objectId });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add ProjectBaseFinishingScoreWithLevel.
        /// </summary>
        /// <param name="createProjectBaseFinishingScoreWithLevel"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateProjectBaseFinishingScoreWithLevelCommand createProjectBaseFinishingScoreWithLevel)
        {
            var result = await Mediator.Send(createProjectBaseFinishingScoreWithLevel);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update ProjectBaseFinishingScoreWithLevel.
        /// </summary>
        /// <param name="updateProjectBaseFinishingScoreWithLevel"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProjectBaseFinishingScoreWithLevelCommand updateProjectBaseFinishingScoreWithLevel)
        {
            var result = await Mediator.Send(updateProjectBaseFinishingScoreWithLevel);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete ProjectBaseFinishingScoreWithLevel.
        /// </summary>
        /// <param name="deleteProjectBaseFinishingScoreWithLevel"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteProjectBaseFinishingScoreWithLevelCommand deleteProjectBaseFinishingScoreWithLevel)
        {
            var result = await Mediator.Send(deleteProjectBaseFinishingScoreWithLevel);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
