using Benaa.Core.Entities.General;
using Benaa.Core.Entities.DTOs;
using Benaa.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Benaa.Infrastructure.Data;

namespace Benaa.Api.Controllers
{
    [Route("api/scheduale")]
    [ApiController]
    public class SchedualeController : ControllerBase
    {
        private readonly IScedualeService _scedualeService;
        private static string ui;
        private readonly UserManager<User> _userManager;

        public SchedualeController(IScedualeService scedualeService, UserManager<User> userManager)
        {
            _scedualeService = scedualeService;
            _userManager = userManager;
        }

        private string GetCurrentUser()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            return userId!;

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _scedualeService.GetSceduales();
                    if(result.IsError) { return BadRequest(result.ErrorsOrEmptyList); }
                    return Ok(result.Value);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return BadRequest();
        }

        [HttpGet("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _scedualeService.GetById(id);
                    if (result.IsError) { return BadRequest(result.ErrorsOrEmptyList); }
                    return Ok(result.Value);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return BadRequest("Please Enter id for Schedual");
        }

        [HttpGet("GetByday")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByday(int day)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ui = GetCurrentUser();
                    var result = await _scedualeService.GetByDay(day,ui);
                    if(result.IsError) { return BadRequest(result.ErrorsOrEmptyList); }
                    return Ok(result.Value);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return BadRequest("Please Enter Specific day");
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Sceduale>> AddSceduales(List<SchedualDto> Sceduales)
        {
            if (ModelState.IsValid)
            {
                ui=GetCurrentUser();
                try
                {
                  var result =  await _scedualeService.AddSceduales(Sceduales, ui);
                    if(result.IsError) { return BadRequest(result.ErrorsOrEmptyList); }
                    return Created("Save successfully", result.Value);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return BadRequest("Please input all required data");
        }


        [HttpPut("Edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(SchedualDetailsDto sc)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   var result = await _scedualeService.UpdateSceduale(sc);
                    if (result.IsError) { return BadRequest(result.ErrorsOrEmptyList); }
                    return Ok(result.Value);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return BadRequest("Please input all required data");
        }

        [HttpPut("BookAppointment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BookAppointment(SchedualDetailsDto Sceduale)
        {
            if (ModelState.IsValid)
            {
                ui = GetCurrentUser();
                try
                {
                    var result = await _scedualeService.BookAppointment(Sceduale, ui);
                    if (result.IsError) { return BadRequest(result.ErrorsOrEmptyList); }
                    return Ok(result.Value);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return BadRequest("Please input all required data");
        }


        [HttpDelete(template: "{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _scedualeService.Delete(id);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return BadRequest("Please input id for Schedual");

        }

        [HttpPost("RepeatSceduale")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RepeatSceduale()
        {
            if (ModelState.IsValid)
            {
                ui = GetCurrentUser();
                try
                {
                    var result = await _scedualeService.RepeatSceduale(ui);
                    if (result.IsError) { return BadRequest(result.ErrorsOrEmptyList); }
                    return Ok(result.Value);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return BadRequest();
        }


    }
}
