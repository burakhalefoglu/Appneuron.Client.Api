
using Business.Handlers.ProjectBaseBuyingCountWithDifficulties.Commands;
using Business.Handlers.ProjectBaseBuyingCountWithDifficulties.Queries;
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
    /// ProjectBaseBuyingCountWithDifficulties If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectBaseBuyingCountWithDifficultiesController : BaseApiController
    {
        ///<summary>
        ///List ProjectBaseBuyingCountWithDifficulties
        ///</summary>
        ///<remarks>ProjectBaseBuyingCountWithDifficulties</remarks>
        ///<return>List ProjectBaseBuyingCountWithDifficulties</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProjectBuyingCountWithDifficulty>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetProjectBaseBuyingCountWithDifficultiesQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>ProjectBaseBuyingCountWithDifficulties</remarks>
        ///<return>ProjectBaseBuyingCountWithDifficulties List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectBuyingCountWithDifficulty))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(string objectId)
        {
            var result = await Mediator.Send(new GetProjectBaseBuyingCountWithDifficultyQuery { ObjectId = objectId });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add ProjectBaseBuyingCountWithDifficulty.
        /// </summary>
        /// <param name="createProjectBaseBuyingCountWithDifficulty"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateProjectBaseBuyingCountWithDifficultyCommand createProjectBaseBuyingCountWithDifficulty)
        {
            var result = await Mediator.Send(createProjectBaseBuyingCountWithDifficulty);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update ProjectBaseBuyingCountWithDifficulty.
        /// </summary>
        /// <param name="updateProjectBaseBuyingCountWithDifficulty"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProjectBaseBuyingCountWithDifficultyCommand updateProjectBaseBuyingCountWithDifficulty)
        {
            var result = await Mediator.Send(updateProjectBaseBuyingCountWithDifficulty);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete ProjectBaseBuyingCountWithDifficulty.
        /// </summary>
        /// <param name="deleteProjectBaseBuyingCountWithDifficulty"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteProjectBaseBuyingCountWithDifficultyCommand deleteProjectBaseBuyingCountWithDifficulty)
        {
            var result = await Mediator.Send(deleteProjectBaseBuyingCountWithDifficulty);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
