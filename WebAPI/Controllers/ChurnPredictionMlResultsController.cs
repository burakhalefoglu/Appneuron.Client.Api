using Business.Handlers.ChurnPredictionMlResultModels.Queries;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Utilities.Results.IResult;

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
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<float>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
    [HttpGet]
    public async Task<IActionResult> Get(long projectId)
    {
        var result = await Mediator.Send(new GetMlResultQuery
        {
            ProjectId = projectId
        });
        if (result.Success) return Ok(result);

        return BadRequest(result);
    }
}