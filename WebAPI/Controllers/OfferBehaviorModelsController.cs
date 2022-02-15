using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Handlers.OfferBehaviorModels.Queries;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    ///     OfferBehaviorModels If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OfferBehaviorModelsController : BaseApiController
    {
        /// <summary>
        ///     List OfferBehaviorModels
        /// </summary>
        /// <remarks>OfferBehaviorModels</remarks>
        /// <return>List OfferBehaviorModels</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<IEnumerable<OfferBehaviorModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
        [HttpGet("getDtoList")]
        public async Task<IActionResult> GetDtoList(long projectId,
            int offerId,
            int version)
        {
            var result = await Mediator.Send(new GetOfferBehaviorDtoQuery
            {
                OfferId = offerId,
                Version = version,
                ProjectId = projectId
            });
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
    }
}