
using Business.Handlers.Clients.Commands;
using Business.Handlers.Clients.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;
using MongoDB.Bson;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClientDataModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("GetListByProject")]
        public async Task<IActionResult> GetListByProject(string ProjectId)
        {
            var result = await Mediator.Send(new GetClientsByProjectIdQuery() {

                ProjectId = ProjectId
            });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}
