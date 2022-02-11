
using Business.Handlers.Clients.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;
using Core.Utilities.Results;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Clients If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : BaseApiController
    {


        ///<summary>
        ///List Clients
        ///</summary>
        ///<remarks>Clients</remarks>
        ///<return>List Clients</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<IEnumerable<ClientDataModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
        [HttpGet("GetListByProject")]
        public async Task<IActionResult> GetListByProject(long projectId)
        {
            var result = await Mediator.Send(new GetClientsByProjectIdQuery() {

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
