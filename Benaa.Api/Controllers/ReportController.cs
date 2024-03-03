using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Benaa.Api.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetReport()
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Sceduale>> Add_Report(ReportDto report)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _report.Report(report);
                    return Ok();
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
