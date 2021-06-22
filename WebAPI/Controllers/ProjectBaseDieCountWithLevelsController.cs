
using Business.Handlers.ProjectBaseDieCountWithLevels.Commands;
using Business.Handlers.ProjectBaseDieCountWithLevels.Queries;
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
    /// ProjectBaseDieCountWithLevels If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectBaseDieCountWithLevelsController : BaseApiController
    {
        ///<summary>
        ///List ProjectBaseDieCountWithLevels
        ///</summary>
        ///<remarks>ProjectBaseDieCountWithLevels</remarks>
        ///<return>List ProjectBaseDieCountWithLevels</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DieCountWithLevel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetProjectBaseDieCountWithLevelsQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>ProjectBaseDieCountWithLevels</remarks>
        ///<return>ProjectBaseDieCountWithLevels List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DieCountWithLevel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(string objectId)
        {
            var result = await Mediator.Send(new GetProjectBaseDieCountWithLevelQuery { ObjectId = objectId });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add ProjectBaseDieCountWithLevel.
        /// </summary>
        /// <param name="createProjectBaseDieCountWithLevel"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateProjectBaseDieCountWithLevelCommand createProjectBaseDieCountWithLevel)
        {
            var result = await Mediator.Send(createProjectBaseDieCountWithLevel);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update ProjectBaseDieCountWithLevel.
        /// </summary>
        /// <param name="updateProjectBaseDieCountWithLevel"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProjectBaseDieCountWithLevelCommand updateProjectBaseDieCountWithLevel)
        {
            var result = await Mediator.Send(updateProjectBaseDieCountWithLevel);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete ProjectBaseDieCountWithLevel.
        /// </summary>
        /// <param name="deleteProjectBaseDieCountWithLevel"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteProjectBaseDieCountWithLevelCommand deleteProjectBaseDieCountWithLevel)
        {
            var result = await Mediator.Send(deleteProjectBaseDieCountWithLevel);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
