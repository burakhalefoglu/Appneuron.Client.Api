
using System;
using Business.Handlers.AdvStrategyBehaviorModels.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Core.Utilities.Results;
namespace WebAPI.Controllers
{
    /// <summary>
    /// AdvStrategyBehaviorModels If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AdvStrategyBehaviorModelsController : BaseApiController
    {
      
        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>AdvStrategyBehaviorModels</remarks>
        ///<return>AdvStrategyBehaviorModels List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<int>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IDataResult<int>))]
        [HttpGet("getByAdvStrategy")]
        public async Task<IActionResult> GetByAdvStrategy(long projectId,
            string name, int version, DateTime time)
        {
            var result = await Mediator.Send(new GetAdvStrategyBehaviorCountByAdvStrategyQuery
            {
                ProjectId = projectId,
                Version = version,
                Name = name,
                StartTime = time
            }
            );
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
   
    }
}
