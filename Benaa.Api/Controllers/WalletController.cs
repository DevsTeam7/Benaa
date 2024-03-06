using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Core.Interfaces.IServices;
using Benaa.Core.Services;
using Benaa.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Benaa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _dbContext;
        private static string ui;
        public WalletController(IWalletService walletService, UserManager<User> userManager, ApplicationDbContext dbContext)
        {
            _walletService = walletService;
            _userManager = userManager;
            _dbContext = dbContext;
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


        [HttpPost("AddWallet")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddWallet(string code)
        {
            try
            {
                if (code == null) { return BadRequest("Please input all required data"); }
                ui = GetCurrentUser();
                return Ok(await _walletService.ChargeWallet(ui, code));
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


        [HttpGet("CheckWallet")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<bool> CheckWallet(string u, decimal price)
        //{
        //    //try
        //    //{
        //        return await _walletService.Check(u,price);
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    //}

        //}
        //public async Task<bool> CheckWallet(string u, decimal price)
        //{ 
        //     bool x =await _walletService.Check(u, price);
        //    return x;
        //}

        public async Task<bool> CheckWallet(string u, decimal price)
        {
             var user = await _dbContext.Users.FirstOrDefaultAsync(s => s.Id == u);
            var wallet = await _dbContext.Wallets.FirstOrDefaultAsync(s => s.Id == user.WalletId);
            decimal amount = wallet.Amount;
 
            if (amount >= price) { return true; }

            return false;
        }

        [HttpPost("Payment")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Payment(Guid itemID,string type,decimal price)
        { ui= GetCurrentUser();
            try
            {
                if (itemID == Guid.Empty|| type==null|| price==0) { return BadRequest("Please input all required data to payent"); }
               
                return Ok(await _walletService.SetPayment( itemID,  type,  price, ui));
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

         
    }
}
