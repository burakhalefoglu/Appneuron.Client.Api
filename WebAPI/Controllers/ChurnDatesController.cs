
using Business.Handlers.ChurnDates.Commands;
using Business.Handlers.ChurnDates.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using Core.Utilities.Results;

namespace WebAPI.Controllers
{
    /// <summary>
    /// ChurnDates If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ChurnDatesController : BaseApiController
    {
        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>ChurnDates</remarks>
        ///<return>ChurnDates List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<ChurnDate>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IDataResult<ChurnDate>))]
        [HttpGet("getByProjectId")]
        public async Task<IActionResult> GetByProjectId(long projectId)
        {
            var result = await Mediator.Send(new GetChurnDateByProjectIdQuery { 
                ProjectId = projectId
            });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        

        /// <summary>
        /// Add ChurnDate.
        /// </summary>
        /// <param name="createChurnDate"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateChurnDateCommand createChurnDate)
        {
            var result = await Mediator.Send(createChurnDate);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Update ChurnDate.
        /// </summary>
        /// <param name="updateChurnDate"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateChurnDateCommand updateChurnDate)
        {
            var result = await Mediator.Send(updateChurnDate);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
