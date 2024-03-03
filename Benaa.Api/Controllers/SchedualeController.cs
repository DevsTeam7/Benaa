using Benaa.Core.Entities.General;
using Benaa.Core.Entities.DTOs;
using Benaa.Core.Interfaces.IServices;
using Benaa.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Benaa.Infrastructure.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Benaa.Core.Interfaces.IRepositories;
using Microsoft.AspNetCore.Identity;
using System.Net.Http;
using Benaa.Infrastructure.Repositories;

namespace Benaa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedualeController : ControllerBase
    {
        private readonly IScedualeService _sc;
        private readonly IWalletService _wallet;
        private readonly ApplicationDbContext _context;
        private static string ui;
        private readonly UserManager<User> _userManager;

        public SchedualeController(IScedualeService sc, ApplicationDbContext context, UserManager<User> userManager, IWalletService wallet)
        {
            _sc = sc;
            _context = context;
            _userManager = userManager;
            _wallet = wallet;
        }

        [HttpGet("GetCurrentUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
                    ui = GetCurrentUser();
                    var scheduale = await _context.Sceduales.ToListAsync();
                    var date = scheduale.Where(x => (x.Date.Day == day)&&(x.TeacherId == ui)).Select(x => new { x.TimeStart, x.TimeEnd });

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
        public async Task<ActionResult<Sceduale>> Add_Schedual(List<SchedualDto> sc)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   await _sc.AddSchedualList(sc);
                    return Ok("Save successfully");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return BadRequest("Please input all required data");
        }
        

        [HttpPut("Edit")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BookAppointment(SchedualDetailsDto sc)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    
                    var Isnull = await _context.Sceduales.AnyAsync(x => (x.StudentId == null ) && (x.Date == sc.Date) && (x.TimeStart == sc.TimeStart));
                    if (Isnull)
                    {
                        ui = GetCurrentUser();
                        //string s_id = sc.StudentId;
                        
                        string type = "schedual";
                        bool R = await _wallet.Check(sc.StudentId, sc.Price);
                        if (R)
                        {                           
                            var user =await _context.Sceduales.FindAsync(sc.Id);
                            user.TeacherId = sc.TeacherId;
                            user.StudentId =sc.StudentId;
                            user.Price = sc.Price;
                            user.Date = sc.Date;
                            user.TimeStart = sc.TimeStart;
                            user.TimeEnd = sc.TimeEnd;

                            _context.Sceduales.Update(user);
                            await _context.SaveChangesAsync();

                            await _wallet.SetPayment(sc.Id, type, sc.Price);
                            return Ok();
                        }
                        return BadRequest("no money in wallet");
                    }
                    return BadRequest("Is Booked");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return BadRequest("Please input all required data");
        }

        
        [HttpDelete(template:"{Id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReaddSchedual()
        {
            DateTime currentDate = DateTime.Now; 
            DateTime startDate = currentDate.AddDays(-7);

            if (ModelState.IsValid)
            {
                try
                {
                    ui = GetCurrentUser();

                    for (int i = 1; i <= 7; i++)
                    {
                        var sc = _context.Sceduales.Where(x => (x.Date == startDate.AddDays(+i)) && (x.TeacherId == ui)).ToList();

                        foreach (var record in sc)
                        {

                            var scheduale = new Sceduale
                            {
                                Date = currentDate.AddDays(+i),
                                TimeStart = record.TimeStart,
                                TimeEnd = record.TimeEnd,
                                Price = record.Price,
                                TeacherId = record.TeacherId,
                                StudentId = record.StudentId,
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
