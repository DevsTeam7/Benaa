using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IServices;
using Benaa.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace Benaa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedualeController : ControllerBase
    {
        WalletController walletController;
        private readonly IScedualeService _sc;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private static string ui;

        public SchedualeController(IScedualeService sc, ApplicationDbContext context, UserManager<User> userManager)
        {
            _sc = sc;
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("GetCurrentUser")]
        public string GetCurrentUser()
        {
            if (User.Identity!.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                //ui = userId!;
                return userId!;
            }
            return "the user is not authenticated";
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int day)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    var scheduale = await _context.Sceduales.ToListAsync();
                    ui = GetCurrentUser();
                    var date = scheduale.Where(x => (x.Date.Day == day) && (x.TeacherId == ui)).Select(x => new { x.TimeStart, x.TimeEnd });

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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
            ui = GetCurrentUser();
            var s_id = ui;
            string type = "student";
             if (ModelState.IsValid)
            {
                try
                {
                        var Isnull = await _context.Sceduales.AnyAsync(x => (x.StudentId == null) && (x.Date == sc.Date) && (x.TimeStart == sc.TimeStart));
                        if (Isnull)
                        {
                            var R = await walletController.CheckWallet(s_id,sc.Price);
                            //if (R == true)
                            //{
                            //    await _sc.Appointment(sc);
                                
                            //    return Ok("Booking successfully");
                            //}
                        return BadRequest("You Don't Have Enough Mony");

                        }

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
