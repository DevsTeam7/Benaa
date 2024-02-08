using Benaa.Core.Entities.General;
using Benaa.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Benaa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class TestController : ControllerBase
    {
        private ApplicationDbContext _context;
        public TestController(ApplicationDbContext applicationDbContext) { _context = applicationDbContext; }
        [HttpGet("Index")]
        public string Index()
        {
            return "done";
        }

        [HttpPost("CreateMonyecode")]
        public void Post()
        {
            MoneyCode moneyCode = new MoneyCode();
            moneyCode.Code = 1111111111111111;
            moneyCode.Status = false;
            _context.MoneyCodes.Add(moneyCode);
            _context.SaveChanges();
        }

        [HttpPut("MyMethod1")]
        public void Pu1t1() { }
        [HttpPut("MyMethod2")]
        public void Put2() { }
    }
}
