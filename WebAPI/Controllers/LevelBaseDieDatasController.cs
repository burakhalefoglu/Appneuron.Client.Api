using Business.Handlers.LevelBaseDieDatas.Commands;
using Business.Handlers.LevelBaseDieDatas.Queries;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    /// <summary>
    /// LevelBaseDieDatas If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LevelBaseDieDatasController : BaseApiController
    {
        ///<summary>
        ///List LevelBaseDieDatas
        ///</summary>
        ///<remarks>LevelBaseDieDatas</remarks>
        ///<return>List LevelBaseDieDatas</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LevelBaseDieData>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getByProjectId")]
        public async Task<IActionResult> GetByProjectId(string ProjectId)
        {
            var result = await Mediator.Send(new GetLevelBaseDieDatasByProjectIdQuery
            {
                ProjectID = ProjectId
            });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///List LevelBaseDieDatas
        ///</summary>
        ///<remarks>LevelBaseDieDatas</remarks>
        ///<return>List LevelBaseDieDatas</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LevelBaseDieData>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getDtoByProjectId")]
        public async Task<IActionResult> GetDtoByProjectId(string ProjectId)
        {
            var result = await Mediator.Send(new GetLevelBaseDieDatasDtoByProjectIdQuery
            {
                ProjectID = ProjectId
            });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }


        /// <summary>
        /// Delete LevelBaseDieData.
        /// </summary>
        /// <param name="deleteLevelBaseDieData"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete("deleteByProjectId")]
        public async Task<IActionResult> DeleteByProjectId([FromBody] DeleteLevelBaseDieDataByProjectIdCommand deleteLevelBaseDieData)
        {
            var result = await Mediator.Send(deleteLevelBaseDieData);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}