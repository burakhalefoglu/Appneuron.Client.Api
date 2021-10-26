
using Business.Handlers.OfferBehaviorModels.Commands;
using Business.Handlers.OfferBehaviorModels.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;
using MongoDB.Bson;
namespace WebAPI.Controllers
{
    /// <summary>
    /// OfferBehaviorModels If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OfferBehaviorModelsController : BaseApiController
    {
        ///<summary>
        ///List OfferBehaviorModels
        ///</summary>
        ///<remarks>OfferBehaviorModels</remarks>
        ///<return>List OfferBehaviorModels</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OfferBehaviorModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetOfferBehaviorModelsQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///List OfferBehaviorModels
        ///</summary>
        ///<remarks>OfferBehaviorModels</remarks>
        ///<return>List OfferBehaviorModels</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OfferBehaviorModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<OfferBehaviorModel>))]
        [HttpGet("getDtoList")]
        public async Task<IActionResult> GetDtoList(string projectId,
            string name,
            int version)
        {
            var result = await Mediator.Send(new GetOfferBehaviorDtoQuery
            {
                Name = name,
                Version = version,
                ProjectId = projectId

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
        ///<remarks>OfferBehaviorModels</remarks>
        ///<return>OfferBehaviorModels List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OfferBehaviorModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(string objectId)
        {
            var result = await Mediator.Send(new GetOfferBehaviorModelQuery { ObjectId = objectId });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add OfferBehaviorModel.
        /// </summary>
        /// <param name="createOfferBehaviorModel"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateOfferBehaviorModelCommand createOfferBehaviorModel)
        {
            var result = await Mediator.Send(createOfferBehaviorModel);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update OfferBehaviorModel.
        /// </summary>
        /// <param name="updateOfferBehaviorModel"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateOfferBehaviorModelCommand updateOfferBehaviorModel)
        {
            var result = await Mediator.Send(updateOfferBehaviorModel);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete OfferBehaviorModel.
        /// </summary>
        /// <param name="deleteOfferBehaviorModel"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteOfferBehaviorModelCommand deleteOfferBehaviorModel)
        {
            var result = await Mediator.Send(deleteOfferBehaviorModel);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
