using Business.Handlers.AdvStrategyBehaviorModels.Commands;
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
public class AdvStrategiesController : BaseApiController
{
    /// <summary>
    ///     AdvStrategyShownCount
    /// </summary>
    /// <remarks></remarks>
    /// <return>AdvStrategyShownCount</return>
    /// <response code="200"></response>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<IEnumerable<AdvStrategyShownCountDto>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
    [HttpPost("AdvStrategyShownCount")]
    public async Task<IActionResult> GetAdvStrategyShownCount(
        [FromBody] AdvStrategyShownCommand advStrategyShownCommand)
    {
        var result = await Mediator.Send(advStrategyShownCommand);
        if (result.Success) return Ok(result);

        return BadRequest(result);
    }
}