using Business.Handlers.LevelBaseDieCountWithDifficulties.Commands;
using Business.Handlers.LevelBaseDieCountWithDifficulties.Queries;
using Entities.Concrete.ChartModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    /// <summary>
    /// LevelBaseDieCountWithDifficulties If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LevelBaseDieCountWithDifficultiesController : BaseApiController
    {
        ///<summary>
        ///List LevelBaseDieCountWithDifficulties
        ///</summary>
        ///<remarks>LevelBaseDieCountWithDifficulties</remarks>
        ///<return>List LevelBaseDieCountWithDifficulties</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LevelBaseDieCountWithDifficulty>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetLevelBaseDieCountWithDifficultiesQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>LevelBaseDieCountWithDifficulties</remarks>
        ///<return>LevelBaseDieCountWithDifficulties List</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LevelBaseDieCountWithDifficulty))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(string objectId)
        {
            var result = await Mediator.Send(new GetLevelBaseDieCountWithDifficultyQuery { ObjectId = objectId });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add LevelBaseDieCountWithDifficulty.
        /// </summary>
        /// <param name="createLevelBaseDieCountWithDifficulty"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateLevelBaseDieCountWithDifficultyCommand createLevelBaseDieCountWithDifficulty)
        {
            var result = await Mediator.Send(createLevelBaseDieCountWithDifficulty);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update LevelBaseDieCountWithDifficulty.
        /// </summary>
        /// <param name="updateLevelBaseDieCountWithDifficulty"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateLevelBaseDieCountWithDifficultyCommand updateLevelBaseDieCountWithDifficulty)
        {
            var result = await Mediator.Send(updateLevelBaseDieCountWithDifficulty);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete LevelBaseDieCountWithDifficulty.
        /// </summary>
        /// <param name="deleteLevelBaseDieCountWithDifficulty"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteLevelBaseDieCountWithDifficultyCommand deleteLevelBaseDieCountWithDifficulty)
        {
            var result = await Mediator.Send(deleteLevelBaseDieCountWithDifficulty);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>LevelBaseDieCountWithDifficulties</remarks>
        ///<return>LevelBaseDieCountWithDifficulties List</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LevelBaseDieCountWithDifficulty))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyprojectid")]
        public async Task<IActionResult> GetByProjectId(string ProjectId)
        {
            var result = await Mediator.Send(new GetLevelBaseDieCountWithDifficultiesByProjectIdQuery { ProjectId = ProjectId });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}