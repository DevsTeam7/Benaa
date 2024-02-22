using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IServices;
using Benaa.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Benaa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;
        private readonly UserManager<User> _userManager;
        private static string ui;
        public WalletController(IWalletService walletService, UserManager<User> userManager)
        {
            _walletService = walletService;
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


        [HttpPost("CheckWallet")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CheckWallet(string u,int price)
        {
            try
            {             
                return Ok(await _walletService.Check( u,  price));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        //[HttpPost("Payment")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> Payment(string code)
        //{
        //    try
        //    {
        //        if (code == null) { return BadRequest("Please input all required data"); }
        //        ui = GetCurrentUser();
        //        return Ok(await _walletService.ChargeWallet(ui, code));
        //    }

        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }

        //}


    }
}
