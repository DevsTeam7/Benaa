using Benaa.Core.Entities.General;
using Benaa.Core.Entities.DTOs;
using Benaa.Core.Interfaces.IServices;
using Benaa.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Benaa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedualeController : ControllerBase
    {
        private readonly IScedualeService _sc;


        public SchedualeController(IScedualeService sc)
        {
            _sc = sc;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var model = await _sc.GetSchedual();
            return Ok(model);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
           var model =  await _sc.GetById(id);
            return Ok(model);
        }

        [HttpPut("BookAppointment")]
        public async Task<IActionResult> BookAppointment(SchedualDetailsDto sc)
        {
            //if (sc == null || sc.Id == id || sc.StudentId != null)
            //{
            //    return BadRequest();
            //}        
            await _sc.Appointment(sc);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Sceduale>> Add_Schedual(SchedualDto sc)
        { 
          var model = await  _sc.AddSchedual(sc);
            return Ok(model);
        }
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(int id , SchedualDetailsDto sc)
        {
            if(sc == null || sc.Id == id)
            {
                return BadRequest();
            }

            await _sc.UpdateSceduale(sc);
            return Ok();
        }


    }
}
