using Business.Handlers.LevelBasePowerUsageWithDifficulties.Commands;
using Business.Handlers.LevelBasePowerUsageWithDifficulties.Queries;
using Entities.Concrete.ChartModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    /// <summary>
    /// LevelBasePowerUsageWithDifficulties If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LevelBasePowerUsageWithDifficultiesController : BaseApiController
    {
        ///<summary>
        ///List LevelBasePowerUsageWithDifficulties
        ///</summary>
        ///<remarks>LevelBasePowerUsageWithDifficulties</remarks>
        ///<return>List LevelBasePowerUsageWithDifficulties</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LevelBasePowerUsageWithDifficulty>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetLevelBasePowerUsageWithDifficultiesQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>LevelBasePowerUsageWithDifficulties</remarks>
        ///<return>LevelBasePowerUsageWithDifficulties List</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LevelBasePowerUsageWithDifficulty))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(string objectId)
        {
            var result = await Mediator.Send(new GetLevelBasePowerUsageWithDifficultyQuery { ObjectId = objectId });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add LevelBasePowerUsageWithDifficulty.
        /// </summary>
        /// <param name="createLevelBasePowerUsageWithDifficulty"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateLevelBasePowerUsageWithDifficultyCommand createLevelBasePowerUsageWithDifficulty)
        {
            var result = await Mediator.Send(createLevelBasePowerUsageWithDifficulty);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update LevelBasePowerUsageWithDifficulty.
        /// </summary>
        /// <param name="updateLevelBasePowerUsageWithDifficulty"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateLevelBasePowerUsageWithDifficultyCommand updateLevelBasePowerUsageWithDifficulty)
        {
            var result = await Mediator.Send(updateLevelBasePowerUsageWithDifficulty);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete LevelBasePowerUsageWithDifficulty.
        /// </summary>
        /// <param name="deleteLevelBasePowerUsageWithDifficulty"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteLevelBasePowerUsageWithDifficultyCommand deleteLevelBasePowerUsageWithDifficulty)
        {
            var result = await Mediator.Send(deleteLevelBasePowerUsageWithDifficulty);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>LevelBasePowerUsageWithDifficulties</remarks>
        ///<return>LevelBasePowerUsageWithDifficulties List</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LevelBasePowerUsageWithDifficulty))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyprojectid")]
        public async Task<IActionResult> GetByProjectId(string ProjectId)
        {
            var result = await Mediator
                .Send(new GetLevelBasePowerUsageWithDifficultiesByProjectIdQuery { ProjectId = ProjectId });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}