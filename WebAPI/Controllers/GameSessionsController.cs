using Business.Handlers.GameSessions.Queries;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Utilities.Results.IResult;

namespace WebAPI.Controllers;

/// <summary>
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class GameSessionsController: BaseApiController
{
    /// <summary>
    ///     daily game session
    /// </summary>
    /// <remarks> </remarks>
    /// <return> daily game session </return>
    /// <response code="200"></response>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<long[]>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
    [HttpGet("GetDailySession")]
    public async Task<IActionResult> GetDailySession(long projectId)
    {
        var result = await Mediator.Send(new GetDailySessionsQuery
        {
            ProjectId = projectId
        });
        if (result.Success) return Ok(result);

        return BadRequest(result);
    }
}