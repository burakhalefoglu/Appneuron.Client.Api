using Business.Handlers.Clients.Queries;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Utilities.Results.IResult;

namespace WebAPI.Controllers;

/// <summary>
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ClientsController : BaseApiController
{
    /// <summary>
    ///     total client count
    /// </summary>
    /// <remarks> </remarks>
    /// <return> total client count</return>
    /// <response code="200"></response>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<long>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
    [HttpGet("GetTotalClient")]
    public async Task<IActionResult> GetTotalClient(long projectId)
    {
        var result = await Mediator.Send(new GetTotalClientCountByProjectIdQuery
        {
            ProjectId = projectId
        });
        if (result.Success) return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    ///     total client count
    /// </summary>
    /// <remarks> </remarks>
    /// <return> total client count</return>
    /// <response code="200"></response>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<long>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
    [HttpGet("GetPaidClient")]
    public async Task<IActionResult> GetPaidClient(long projectId)
    {
        var result = await Mediator.Send(new GetPaidClientCountByProjectIdQuery
        {
            ProjectId = projectId
        });
        if (result.Success) return Ok(result);

        return BadRequest(result);
    }
}