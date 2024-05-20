using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Benaa.Api.Controllers
{
    [Route("api/report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _report;
        private readonly UserManager<User> _userManager;


        public ReportController(IReportService report, UserManager<User> userManager)
        {
            _report = report;
            _userManager = userManager;
        }

        [HttpGet("Get")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var model = await _report.GetReport();
                    return Ok(model);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return BadRequest();
        }

        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Sceduale>> Create(ReportDto report)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string userId = _userManager.GetUserId(HttpContext.User)!;
                    var result = await _report.Report(report, userId);
                    if (result.IsError) { return BadRequest(result.ErrorsOrEmptyList); }
                    return Created("", result.Value);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return BadRequest("Please input all required data");
        }

    }
}
