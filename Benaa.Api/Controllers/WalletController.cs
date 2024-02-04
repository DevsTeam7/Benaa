using Benaa.Core.Interfaces.IServices;
using Benaa.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Benaa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService) {
            _walletService = walletService;
        }
        [HttpGet]
        public string Payment(decimal Amount)
        {
          return  _walletService.IsPayed(Amount);
        }
    }
}
