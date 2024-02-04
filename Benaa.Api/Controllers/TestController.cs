using Benaa.Core.Entities.General;
using Benaa.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Benaa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
       // private readonly ILogger _logger;
        public TestController(ApplicationDbContext context) {
            //_logger = logger;
            _context = context;
        }
        [HttpGet]
        public string Get()
        {
            GuidTest a = new GuidTest();
            a.File = "123";
            _context.GuidTests.Add(a);
            _context.SaveChanges();
            return "yo";
        }
    }
}
