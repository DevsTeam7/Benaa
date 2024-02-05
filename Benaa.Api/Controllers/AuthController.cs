using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IServices;
using Benaa.Core.Entities.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Benaa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register(RegisterRequestDto newUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User user = await _authService.Registration(newUser);
                    if (user == null) { return BadRequest("Faild to create the user! try again"); }
                    return Created("", user);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return BadRequest("Please input all required data");
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(LoginRequestDto applictionUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userExist = await _authService.Login(applictionUser);
                    if (!string.IsNullOrEmpty(userExist)) return Ok(userExist);
                    return Unauthorized("The user does not exist");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return BadRequest("Please input all required data");
        }
    }
}
