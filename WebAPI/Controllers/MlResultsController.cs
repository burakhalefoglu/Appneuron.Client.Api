
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;
using Business.Handlers.MlResults.Queries;
using Core.Utilities.Results;

namespace WebAPI.Controllers
{
    /// <summary>
    /// MlResultModels If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MlResultsController : BaseApiController
    {

        ///<summary>
        ///List MlResultModels
        ///</summary>
        ///<remarks>MlResultModels</remarks>
        ///<return>List MlResultModels</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<IEnumerable<ChurnBlokerMlResult>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
        [HttpGet("getLisByProductIdAndProjectId")]
        public async Task<IActionResult> GetListByProductIdAndProjectId(long projectId, int productId)
        {
            var result = await Mediator.Send(new GetMlResultByProjectAndProductIdQuery() { 
            
                ProductId = productId,
                ProjectId = projectId

            });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
