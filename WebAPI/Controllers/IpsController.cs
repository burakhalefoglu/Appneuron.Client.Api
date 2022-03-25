using Business.Handlers;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Utilities.Results.IResult;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IpsController: BaseApiController
{
    /// <summary>
    ///     List Logs
    /// </summary>
    /// <remarks>bla bla bla Logs</remarks>
    /// <return>Logs List</return>
    /// <response code="200"></response>
    [AllowAnonymous]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
    [HttpGet()]
    public Task<IActionResult> Get()
    {
        var result = Mediator.Send(new GetIpQuery());
        return Task.FromResult<IActionResult>(Ok(result));
    }
}
