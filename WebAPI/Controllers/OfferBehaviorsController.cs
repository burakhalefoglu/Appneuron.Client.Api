using Business.Handlers.OfferBehaviorModels.Commands;
using Core.Utilities.Results;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Utilities.Results.IResult;

namespace WebAPI.Controllers;

/// <summary>
///     If controller methods will not be Authorize, [AllowAnonymous] is used.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class OfferBehaviorsController : BaseApiController
{
    /// <summary>
    ///    Offer behavior success
    /// </summary>
    /// <remarks></remarks>
    /// <return>offerBehaviors List</return>
    /// <response code="200"></response>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<IEnumerable<OfferBehaviorSuccessDto>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
    [HttpPost("OfferBehaviorSuccess")]
    public async Task<IActionResult> GetOfferBehaviorSuccessQuery([FromBody] OfferBehaviorSuccessCommand offerBehaviorSuccessCommand)
    {
        var result = await Mediator.Send(offerBehaviorSuccessCommand);
        if (result.Success) return Ok(result);

        return BadRequest(result);
    }
}