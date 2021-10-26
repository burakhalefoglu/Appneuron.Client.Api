
using System;
using Business.Handlers.AdvStrategyBehaviorModels.Commands;
using Business.Handlers.AdvStrategyBehaviorModels.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;
using Core.Utilities.Results;
using MongoDB.Bson;
namespace WebAPI.Controllers
{
    /// <summary>
    /// AdvStrategyBehaviorModels If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AdvStrategyBehaviorModelsController : BaseApiController
    {
        ///<summary>
        ///List AdvStrategyBehaviorModels
        ///</summary>
        ///<remarks>AdvStrategyBehaviorModels</remarks>
        ///<return>List AdvStrategyBehaviorModels</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AdvStrategyBehaviorModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetAdvStrategyBehaviorModelsQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>AdvStrategyBehaviorModels</remarks>
        ///<return>AdvStrategyBehaviorModels List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AdvStrategyBehaviorModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(string objectId)
        {
            var result = await Mediator.Send(new GetAdvStrategyBehaviorModelQuery { ObjectId = objectId });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>AdvStrategyBehaviorModels</remarks>
        ///<return>AdvStrategyBehaviorModels List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<int>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IDataResult<int>))]
        [HttpGet("getByAdvStrategy")]
        public async Task<IActionResult> GetByAdvStrategy(string projectId,
            string name, int version, DateTime time)
        {
            var result = await Mediator.Send(new GetAdvStrategyBehaviorCountByAdvStrategyQuery
            {
                ProjectId = projectId,
                Version = version,
                Name = name,
                StartTime = time
            }
            );
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Add AdvStrategyBehaviorModel.
        /// </summary>
        /// <param name="createAdvStrategyBehaviorModel"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateAdvStrategyBehaviorModelCommand createAdvStrategyBehaviorModel)
        {
            var result = await Mediator.Send(createAdvStrategyBehaviorModel);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update AdvStrategyBehaviorModel.
        /// </summary>
        /// <param name="updateAdvStrategyBehaviorModel"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateAdvStrategyBehaviorModelCommand updateAdvStrategyBehaviorModel)
        {
            var result = await Mediator.Send(updateAdvStrategyBehaviorModel);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete AdvStrategyBehaviorModel.
        /// </summary>
        /// <param name="deleteAdvStrategyBehaviorModel"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteAdvStrategyBehaviorModelCommand deleteAdvStrategyBehaviorModel)
        {
            var result = await Mediator.Send(deleteAdvStrategyBehaviorModel);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
