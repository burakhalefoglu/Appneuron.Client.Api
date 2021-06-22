
using Business.Handlers.ProjectBaseSuccessAttemptRates.Commands;
using Business.Handlers.ProjectBaseSuccessAttemptRates.Queries;
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
    /// ProjectBaseSuccessAttemptRates If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectBaseSuccessAttemptRatesController : BaseApiController
    {
        ///<summary>
        ///List ProjectBaseSuccessAttemptRates
        ///</summary>
        ///<remarks>ProjectBaseSuccessAttemptRates</remarks>
        ///<return>List ProjectBaseSuccessAttemptRates</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SuccessAttemptRate>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetProjectBaseSuccessAttemptRatesQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>ProjectBaseSuccessAttemptRates</remarks>
        ///<return>ProjectBaseSuccessAttemptRates List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessAttemptRate))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(string objectId)
        {
            var result = await Mediator.Send(new GetProjectBaseSuccessAttemptRateQuery { ObjectId = objectId });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add ProjectBaseSuccessAttemptRate.
        /// </summary>
        /// <param name="createProjectBaseSuccessAttemptRate"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateProjectBaseSuccessAttemptRateCommand createProjectBaseSuccessAttemptRate)
        {
            var result = await Mediator.Send(createProjectBaseSuccessAttemptRate);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update ProjectBaseSuccessAttemptRate.
        /// </summary>
        /// <param name="updateProjectBaseSuccessAttemptRate"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProjectBaseSuccessAttemptRateCommand updateProjectBaseSuccessAttemptRate)
        {
            var result = await Mediator.Send(updateProjectBaseSuccessAttemptRate);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete ProjectBaseSuccessAttemptRate.
        /// </summary>
        /// <param name="deleteProjectBaseSuccessAttemptRate"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteProjectBaseSuccessAttemptRateCommand deleteProjectBaseSuccessAttemptRate)
        {
            var result = await Mediator.Send(deleteProjectBaseSuccessAttemptRate);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
