
using Business.Handlers.OfferBehaviorModels.Queries;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<IEnumerable<OfferBehaviorModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
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
    }
}
