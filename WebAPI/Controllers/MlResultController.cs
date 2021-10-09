
using Business.Handlers.MlResultModels.Commands;
using Business.Handlers.MlResultModels.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;
using MongoDB.Bson;
using Business.Handlers.MlResults.Queries;

namespace WebAPI.Controllers
{
    /// <summary>
    /// MlResultModels If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MlResultController : BaseApiController
    {
        ///<summary>
        ///List MlResultModels
        ///</summary>
        ///<remarks>MlResultModels</remarks>
        ///<return>List MlResultModels</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ChurnBlokerMlResult>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetMlResultModelsQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>MlResultModels</remarks>
        ///<return>MlResultModels List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ChurnBlokerMlResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(string objectId)
        {
            var result = await Mediator.Send(new GetMlResultModelQuery { ObjectId = objectId });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///List MlResultModels
        ///</summary>
        ///<remarks>MlResultModels</remarks>
        ///<return>List MlResultModels</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ChurnBlokerMlResult>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getListbyProductIdAndProjectId")]
        public async Task<IActionResult> GetListbyProductIdAndProjectId(string ProjectId, int ProductId)
        {
            var result = await Mediator.Send(new GetMlResultByProjectAndProductIdQuery() { 
            
                ProductId = ProductId,
                ProjectId = ProjectId

            });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        
        /// <summary>
        /// Add MlResultModel.
        /// </summary>
        /// <param name="createMlResultModel"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateMlResultModelCommand createMlResultModel)
        {
            var result = await Mediator.Send(createMlResultModel);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update MlResultModel.
        /// </summary>
        /// <param name="updateMlResultModel"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateMlResultModelCommand updateMlResultModel)
        {
            var result = await Mediator.Send(updateMlResultModel);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete MlResultModel.
        /// </summary>
        /// <param name="deleteMlResultModel"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteMlResultModelCommand deleteMlResultModel)
        {
            var result = await Mediator.Send(deleteMlResultModel);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
