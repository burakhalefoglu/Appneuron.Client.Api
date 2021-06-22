
using Business.Handlers.ProjectBaseTotalDieWithDifficulties.Commands;
using Business.Handlers.ProjectBaseTotalDieWithDifficulties.Queries;
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
    /// ProjectBaseTotalDieWithDifficulties If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectBaseTotalDieWithDifficultiesController : BaseApiController
    {
        ///<summary>
        ///List ProjectBaseTotalDieWithDifficulties
        ///</summary>
        ///<remarks>ProjectBaseTotalDieWithDifficulties</remarks>
        ///<return>List ProjectBaseTotalDieWithDifficulties</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProjectTotalDieWithDifficulty>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetProjectBaseTotalDieWithDifficultiesQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>ProjectBaseTotalDieWithDifficulties</remarks>
        ///<return>ProjectBaseTotalDieWithDifficulties List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectTotalDieWithDifficulty))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(string objectId)
        {
            var result = await Mediator.Send(new GetProjectBaseTotalDieWithDifficultyQuery { ObjectId = objectId });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add ProjectBaseTotalDieWithDifficulty.
        /// </summary>
        /// <param name="createProjectBaseTotalDieWithDifficulty"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateProjectBaseTotalDieWithDifficultyCommand createProjectBaseTotalDieWithDifficulty)
        {
            var result = await Mediator.Send(createProjectBaseTotalDieWithDifficulty);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update ProjectBaseTotalDieWithDifficulty.
        /// </summary>
        /// <param name="updateProjectBaseTotalDieWithDifficulty"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProjectBaseTotalDieWithDifficultyCommand updateProjectBaseTotalDieWithDifficulty)
        {
            var result = await Mediator.Send(updateProjectBaseTotalDieWithDifficulty);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete ProjectBaseTotalDieWithDifficulty.
        /// </summary>
        /// <param name="deleteProjectBaseTotalDieWithDifficulty"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteProjectBaseTotalDieWithDifficultyCommand deleteProjectBaseTotalDieWithDifficulty)
        {
            var result = await Mediator.Send(deleteProjectBaseTotalDieWithDifficulty);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
