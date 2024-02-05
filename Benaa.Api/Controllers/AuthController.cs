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
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(RegisterRequestDto user)
        {
            return Ok(await _authService.RegisterUser(user));
        }

        [HttpPost("Login")]
        public async Task<string> Login(RegisterRequestDto user)
        {
            if(ModelState.IsValid)
            {
                if (await _authService.Login(user)) { return _authService.GenerateTokenString(user); }
       
            }
            return null;
        }
    }
}
