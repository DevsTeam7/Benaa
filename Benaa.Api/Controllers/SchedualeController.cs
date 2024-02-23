using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IServices;
using Benaa.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Benaa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedualeController : ControllerBase
    {
        private readonly IScedualeService _sc;
        private readonly ApplicationDbContext _context;
 

        public SchedualeController(IScedualeService sc, ApplicationDbContext context)
        {
            _sc = sc;
            _context = context;
         }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var model = await _sc.GetSchedual();
                    return Ok(model);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return BadRequest();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var model = await _sc.GetById(id);
                    return Ok(model);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return BadRequest("Please Enter id for Schedual");
        }

        [HttpGet("day")]
        public async Task<IActionResult> GetById(int day)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var scheduale = await _context.Sceduales.ToListAsync();
                    var date = scheduale.Where(x => x.Date.Day == day).Select(x => new { x.TimeStart, x.TimeEnd });

                    return Ok(date);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return BadRequest("Please Enter Specific day");
        }

        [HttpPost]
        public IActionResult AddSchedual(List<Sceduale> sc)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _sc.AddSchedualList(sc);
                    return Ok("Save successfully");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return BadRequest("Please input all required data");
        }
        //public async Task<ActionResult<Sceduale>> Add_Schedual(SchedualDto sc)
        //{
        //            var model = await _sc.AddSchedual(sc);
        //            return Ok(model);
        //        }
        //}

        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(SchedualDetailsDto sc)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _sc.UpdateSceduale(sc);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return BadRequest("Please input all required data");
        }

     
        [HttpPut("BookAppointment")]
        public async Task<IActionResult> BookAppointment(SchedualDetailsDto sc)
        {
             Sceduale sceduale = new Sceduale();
             Wallet wallet = new Wallet ();
            sceduale.SetWallet(wallet);
            //bool hasPrice = sceduale.SendRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    //if (hasPrice)
                    //{
                        var Isnull = await _context.Sceduales.AnyAsync(x => (x.StudentId == null) && (x.Date == sc.Date) && (x.TimeStart == sc.TimeStart));
                        if (Isnull)
                        {
                            await _sc.Appointment(sc);
                            return Ok("Booking successfully");
                        }
                    //}
                    //else { return BadRequest("You Don't Have Enough Mony "); }

                    return BadRequest("Sory Is Booked");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return BadRequest("Please input all required data");
        }

        
        [HttpDelete(template:"{Id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _sc.Delete(id);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return BadRequest("Please input id for Schedual");

        }
        

        [HttpPost("Readd")]
        public async Task<IActionResult> ReaddSchedual(string TeacherId)
        {
            DateTime currentDate = DateTime.Now; 
            DateTime startDate = currentDate.AddDays(-7);

            if (ModelState.IsValid)
            {
                try
                {
                    for (int i = 1; i <= 7; i++)
                    {
                        var sc = _context.Sceduales.Where(x => (x.Date == startDate.AddDays(+i)) && (x.TeacherId == TeacherId)).ToList();

                        foreach (var record in sc)
                        {
                            var scheduale = new Sceduale
                            {
                                Date = currentDate.AddDays(+i),
                                TimeStart = record.TimeStart,
                                TimeEnd = record.TimeEnd,
                                Price = record.Price,
                                TeacherId = record.TeacherId,
                                StudentId = null
                            };
                            await _context.Sceduales.AddAsync(scheduale);
                            _context.SaveChanges();
                        }
                    }
                    return Ok();
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
