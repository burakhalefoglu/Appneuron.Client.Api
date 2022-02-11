
using Business.Handlers.ChurnClientPredictionResults.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Core.Utilities.Results;
using System;

namespace WebAPI.Controllers
{
    /// <summary>
    /// ChurnClientPredictionResults If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ChurnClientPredictionResultsController : BaseApiController
    {
        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>ChurnClientPredictionResults</remarks>
        ///<return>ChurnClientPredictionResults List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<int>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IDataResult<int>))]
        [HttpGet("getPredictionCountByDate")]
        public async Task<IActionResult> GetPredictionCountByOfferDate(
            long projectId,
            DateTime startTime,
            DateTime finishTime)
        {
            var result = await Mediator.Send(new GetChurnClientCountByDateQuery
            {
                ProjectId = projectId,
                StartTime = startTime,
                FinishTime = finishTime
            });
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
