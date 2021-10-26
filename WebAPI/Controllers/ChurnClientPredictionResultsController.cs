
using Business.Handlers.ChurnClientPredictionResults.Commands;
using Business.Handlers.ChurnClientPredictionResults.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;
using MongoDB.Bson;
using Core.Utilities.Results;
using Entities.Dtos;
using System;

namespace WebAPI.Controllers
{
    /// <summary>
    /// ChurnClientPredictionResults If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ChurnClientPredictionResultsController : BaseApiController
    {
        ///<summary>
        ///List ChurnClientPredictionResults
        ///</summary>
        ///<remarks>ChurnClientPredictionResults</remarks>
        ///<return>List ChurnClientPredictionResults</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ChurnClientPredictionResult>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetChurnClientPredictionResultsQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>ChurnClientPredictionResults</remarks>
        ///<return>ChurnClientPredictionResults List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ChurnClientPredictionResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(string objectId)
        {
            var result = await Mediator.Send(new GetChurnClientPredictionResultQuery { ObjectId = objectId });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>ChurnClientPredictionResults</remarks>
        ///<return>ChurnClientPredictionResults List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<int>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IDataResult<int>))]
        [HttpGet("getPredictionCountByOfferDate")]
        public async Task<IActionResult> GetPredictionCountByOfferDate(
            string projectId,
            string name,
            int version,
            DateTime startTime,
            DateTime finishTime)
        {
            var result = await Mediator.Send(new GetChurnClientCountByOfferQuery
            {
                ProjectId = projectId,
                Name = name,
                Version = version,
                StartTime = startTime,
                FinishTime = finishTime
            });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }   
        
        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>ChurnClientPredictionResults</remarks>
        ///<return>ChurnClientPredictionResults List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<int>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IDataResult<int>))]
        [HttpGet("getPredictionCountByDate")]
        public async Task<IActionResult> GetPredictionCountByOfferDate(
            string projectId,
            DateTime startTime,
            DateTime finishTime)
        {
            var result = await Mediator.Send(new GetChurnClientCountByDateQuery
            {
                ProjectId = projectId,
                StartTime = startTime,
                FinishTime = finishTime
            });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Add ChurnClientPredictionResult.
        /// </summary>
        /// <param name="createChurnClientPredictionResult"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateChurnClientPredictionResultCommand createChurnClientPredictionResult)
        {
            var result = await Mediator.Send(createChurnClientPredictionResult);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update ChurnClientPredictionResult.
        /// </summary>
        /// <param name="updateChurnClientPredictionResult"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateChurnClientPredictionResultCommand updateChurnClientPredictionResult)
        {
            var result = await Mediator.Send(updateChurnClientPredictionResult);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete ChurnClientPredictionResult.
        /// </summary>
        /// <param name="deleteChurnClientPredictionResult"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteChurnClientPredictionResultCommand deleteChurnClientPredictionResult)
        {
            var result = await Mediator.Send(deleteChurnClientPredictionResult);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
