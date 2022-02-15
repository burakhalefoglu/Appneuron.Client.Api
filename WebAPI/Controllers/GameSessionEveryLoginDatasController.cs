﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Handlers.GameSessionModels.Commands;
using Business.Handlers.GameSessionModels.Queries;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    ///     GameSessionEveryLoginDatas If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GameSessionEveryLoginDatasController : BaseApiController
    {
        /// <summary>
        ///     List GameSessionEveryLoginDatas
        /// </summary>
        /// <remarks>GameSessionEveryLoginDatas</remarks>
        /// <return>List GameSessionEveryLoginDatas</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK,
            Type = typeof(IDataResult<IEnumerable<GameSessionModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
        [HttpGet("getByProjectId")]
        public async Task<IActionResult> GetByProjectId(long projectId)
        {
            var result = await Mediator.Send(new GetGameSessionModelByProjectIdQuery
            {
                ProjectId = projectId
            });
            if (result.Success) return Ok(result.Data);
            return BadRequest(result.Message);
        }

        /// <summary>
        ///     List GameSessionEveryLoginDatas
        /// </summary>
        /// <remarks>GameSessionEveryLoginDatas</remarks>
        /// <return>List GameSessionEveryLoginDatas</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK,
            Type = typeof(IDataResult<IEnumerable<RetentionDataWithSessionDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
        [HttpGet("getRetentionDataByProjectId")]
        public async Task<IActionResult> GetRetentionDataDtoByProjectId(long projectId)
        {
            var result = await Mediator.Send(new GetRetentionDataDtoByProjectIdQuery
            {
                ProjectId = projectId
            });
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        ///     Delete GameSessionEveryLoginData.
        /// </summary>
        /// <param name="deleteGameSessionEveryLoginData"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
        [HttpDelete("deleteByProjectId")]
        public async Task<IActionResult> DeleteByProjectId(
            [FromBody] DeleteGameSessionModelByProjectIdCommand deleteGameSessionEveryLoginData)
        {
            var result = await Mediator.Send(deleteGameSessionEveryLoginData);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
    }
}