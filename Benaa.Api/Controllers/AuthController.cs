using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IServices;
using Benaa.Core.Entities.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Benaa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) {
            _authService = authService;
        }
        [HttpPost]
        public async Task<IEnumerable<IdentityError>> RegisterUser(RegisterRequestDto user)
        {
            return await _authService.RegisterUser(user);
        }
        [HttpGet]
        public async Task Login(User user)
        {

        }
    }
}
