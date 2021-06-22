
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;
using Business.Handlers.PowerUsageByDifficulties.Queries;
using Business.Handlers.PowerUsageByDifficulties.Commands;

namespace WebAPI.Controllers
{
    /// <summary>
    /// ProjectBasePowerUsageByDifficulties If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PowerUsageByDifficultiesController : BaseApiController
    {
        ///<summary>
        ///List ProjectBasePowerUsageByDifficulties
        ///</summary>
        ///<remarks>ProjectBasePowerUsageByDifficulties</remarks>
        ///<return>List ProjectBasePowerUsageByDifficulties</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PowerUsageByDifficulty>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetProjectBasePowerUsageByDifficultiesQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>ProjectBasePowerUsageByDifficulties</remarks>
        ///<return>ProjectBasePowerUsageByDifficulties List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PowerUsageByDifficulty))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(string objectId)
        {
            var result = await Mediator.Send(new GetProjectBasePowerUsageByDifficultyQuery { ObjectId = objectId });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add ProjectBasePowerUsageByDifficulty.
        /// </summary>
        /// <param name="createProjectBasePowerUsageByDifficulty"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateProjectBasePowerUsageByDifficultyCommand createProjectBasePowerUsageByDifficulty)
        {
            var result = await Mediator.Send(createProjectBasePowerUsageByDifficulty);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update ProjectBasePowerUsageByDifficulty.
        /// </summary>
        /// <param name="updateProjectBasePowerUsageByDifficulty"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProjectBasePowerUsageByDifficultyCommand updateProjectBasePowerUsageByDifficulty)
        {
            var result = await Mediator.Send(updateProjectBasePowerUsageByDifficulty);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete ProjectBasePowerUsageByDifficulty.
        /// </summary>
        /// <param name="deleteProjectBasePowerUsageByDifficulty"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteProjectBasePowerUsageByDifficultyCommand deleteProjectBasePowerUsageByDifficulty)
        {
            var result = await Mediator.Send(deleteProjectBasePowerUsageByDifficulty);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
