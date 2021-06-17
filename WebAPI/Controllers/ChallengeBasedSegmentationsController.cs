
using Business.Handlers.ChallengeBasedSegmentations.Commands;
using Business.Handlers.ChallengeBasedSegmentations.Queries;
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
    /// ChallengeBasedSegmentations If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ChallengeBasedSegmentationsController : BaseApiController
    {
        ///<summary>
        ///List ChallengeBasedSegmentations
        ///</summary>
        ///<remarks>ChallengeBasedSegmentations</remarks>
        ///<return>List ChallengeBasedSegmentations</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProjectAndChallengeBasedSegmentation>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetChallengeBasedSegmentationsQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>ChallengeBasedSegmentations</remarks>
        ///<return>ChallengeBasedSegmentations List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectAndChallengeBasedSegmentation))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(string objectId)
        {
            var result = await Mediator.Send(new GetChallengeBasedSegmentationQuery { ObjectId = objectId });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add ChallengeBasedSegmentation.
        /// </summary>
        /// <param name="createChallengeBasedSegmentation"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateChallengeBasedSegmentationCommand createChallengeBasedSegmentation)
        {
            var result = await Mediator.Send(createChallengeBasedSegmentation);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update ChallengeBasedSegmentation.
        /// </summary>
        /// <param name="updateChallengeBasedSegmentation"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateChallengeBasedSegmentationCommand updateChallengeBasedSegmentation)
        {
            var result = await Mediator.Send(updateChallengeBasedSegmentation);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete ChallengeBasedSegmentation.
        /// </summary>
        /// <param name="deleteChallengeBasedSegmentation"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteChallengeBasedSegmentationCommand deleteChallengeBasedSegmentation)
        {
            var result = await Mediator.Send(deleteChallengeBasedSegmentation);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
