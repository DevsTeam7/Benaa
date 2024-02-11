using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Benaa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<User> _userManager;

        public AuthController(IAuthService authService, UserManager<User> userManager)
        {
            _authService = authService;
            _userManager = userManager;
         
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
                    //TODO: Check if the user exist and the password is incorrect
                    return Unauthorized("The email or password is incorrect");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return BadRequest("Please input all required data");
        }

        [HttpGet("GetCurrentUser")]
        public string GetCurrentUser()
        {
            if (User.Identity!.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                return userId!;
            }
            return "the user is not authenticated";
        }
    }
}
