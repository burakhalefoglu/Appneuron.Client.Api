using Business.Handlers.GameSessions.Queries;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Utilities.Results.IResult;

namespace WebAPI.Controllers;

/// <summary>
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class GameSessionsController : BaseApiController
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

    /// <summary>
    ///     retention
    /// </summary>
    /// <remarks> </remarks>
    /// <return> retention </return>
    /// <response code="200"></response>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<long[]>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
    [HttpGet("GetRetention")]
    public async Task<IActionResult> GetRetention(long projectId, string date)
    {
        var result = await Mediator.Send(new GetRetentionQuery
        {
            ProjectId = projectId,
            SessionDate = Convert.ToDateTime(date)
        });
        if (result.Success) return Ok(result);

        return BadRequest(result);
    }


    /// <summary>
    ///     retention
    /// </summary>
    /// <remarks> </remarks>
    /// <return> retention </return>
    /// <response code="200"></response>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<long[]>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
    [HttpGet("GetSessionTimeByDate")]
    public async Task<IActionResult> GetSessionTimeByDate(long projectId, long date)
    {
        var result = await Mediator.Send(new GetSessionTimeByDateQuery
        {
            ProjectId = projectId,
            Date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddMilliseconds(date)
                .ToLocalTime()
        });
        if (result.Success) return Ok(result);

        return BadRequest(result);
    }
}