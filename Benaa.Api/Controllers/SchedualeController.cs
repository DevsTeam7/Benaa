using Benaa.Core.Entities.General;
using Benaa.Core.Entities.DTOs;
using Benaa.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Benaa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Benaa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedualeController : ControllerBase
    {
        private readonly IScedualeService _sc;
        private readonly IWalletService _wallet;
        private readonly IChatHubService _chatHubService;
        private readonly ApplicationDbContext _context;
        private static string ui;
        private readonly UserManager<User> _userManager;

        public SchedualeController(IScedualeService sc
            , ApplicationDbContext context,
            UserManager<User> userManager,
            IWalletService wallet,
            IChatHubService chatHubService)
        {
            _sc = sc;
            _context = context;
            _userManager = userManager;
            _wallet = wallet;
            _chatHubService = chatHubService;
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
        public async Task<IActionResult> BookAppointment(SchedualDetailsDto Sceduale)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //TODO : make this come form then Sceduales services and filtter the Sceduale using the id
                    var Isnull = await _context.Sceduales.AnyAsync(sceduale => (sceduale.StudentId == null) && (sceduale.Date == Sceduale.Date) && (sceduale.TimeStart == Sceduale.TimeStart));
                    if (Isnull)
                    {
                        ui = GetCurrentUser();

                        string type = "schedual";
                        //TODO : make this come form then Sceduales services and try to make it single query beucz this take time
                        var amount = await _context.Users.Where(u => u.Id == ui)
                            .Select(user => _context.Wallets.FirstOrDefault(userWallet => userWallet.Id == user.WalletId))
                            .Where(Wallet => Wallet != null).Select(Wallet => Wallet.Amount).FirstOrDefaultAsync();

                        if (amount >= Sceduale.Price)
                        {
                            var sceduale = await _context.Sceduales.FindAsync(Sceduale.Id);
                            sceduale.TeacherId = Sceduale.TeacherId;
                            sceduale.StudentId = ui;
                            sceduale.Price = Sceduale.Price;
                            sceduale.Date = Sceduale.Date;
                            sceduale.TimeStart = Sceduale.TimeStart;
                            sceduale.TimeEnd = Sceduale.TimeEnd;

                            _context.Sceduales.Update(sceduale);
                            await _context.SaveChangesAsync();
                            var payment = await _wallet.SetPayment(Sceduale.Id, type, Sceduale.Price, ui);

                            await _chatHubService.CreateChat(ui, sceduale.TeacherId, sceduale.Id);

                            return Ok(payment);
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


        [HttpDelete(template: "{Id}")]
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
