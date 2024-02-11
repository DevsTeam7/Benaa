using Benaa.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Benaa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyCodeController : ControllerBase
    {
        private readonly IMoneyCodeService _moneycodeService;

        public MoneyCodeController(IMoneyCodeService moneycodeService)
        {
            _moneycodeService = moneycodeService;
        }

        [HttpPost("GenerateCodes")]
        public async Task<IActionResult> GenerateCode(int amount, int number)
        {
            
                return Ok(await _moneycodeService.Generate(amount, number)); 
                       
            

        }


        
    }
}
