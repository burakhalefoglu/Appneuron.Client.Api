using Business.Handlers.ChurnPredictionMlResultModels.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ChurnPredictionMlResultsController : BaseApiController
{
    /// <summary>
    /// </summary>
    /// <returns></returns>
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
    [HttpGet()]
    public async Task<IActionResult> CheckHealth(long projectId)
    {
        var result = await Mediator.Send(new GetMlResultQuery
        {
            ProjectId = projectId
        });
        if (result.Success) return Ok(result);

        return BadRequest(result);
    }
}