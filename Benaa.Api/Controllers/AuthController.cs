using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IServices;
using Benaa.Core.Utils.FileUploadTypes;
using ErrorOr;
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
        private readonly IFileUploadService _fileUploadService;

        public AuthController(IAuthService authService, UserManager<User> userManager,IFileUploadService fileUploadService)
        {
            _authService = authService;
            _userManager = userManager;
            _fileUploadService = fileUploadService;
        }

        [HttpPost("RegisterStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterStudent([FromForm] StudentRegisterDto newUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ErrorOr<User> result = await _authService.RegisterStudent(newUser);
                    if (result.IsError) { return BadRequest(result.Errors); }
                    else { return Created("Student Created", result); }
                     
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return BadRequest("Please input all required filds");
        }

        [HttpPost("RegisterTeacher")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterTeacher([FromForm] TeacherRegisterDto newTeacher)
        {
            if (ModelState.IsValid)
            {
                var fileExtension = _fileUploadService.GetFileExtension(newTeacher.Certifications.FileName);
                if (PhoneUploadFile.FileExtensions.Contains(fileExtension) is false)
                {
                    return BadRequest("Please Check File Type");
                }
                try
                {
                    ErrorOr<User> result = await _authService.RegisterTeacher(newTeacher);
                    if (result.IsError) { return BadRequest(result.Errors); }
                    else { return Created("Student Created", result); }

                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return BadRequest("Please input all required filds");
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LoginRequestDto.Response>> Login(LoginRequestDto.Request applictionUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _authService.Login(applictionUser);
                    if(result.IsError) { return Unauthorized("The email or password is incorrect"); }
                    else { return Ok(result); }
                    //if (!string.IsNullOrEmpty(userExist)) return Ok(userExist);
                    //TODO: Check if the user exist and the password is incorrect

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
