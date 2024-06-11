using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IServices;
using Benaa.Core.Utils.FileUploadTypes;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Benaa.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<User> _userManager;
        private readonly IFileUploadService _fileUploadService;
        private readonly IOTPService _otpService;
        private readonly IEmailService _emailService;
        private readonly INotificationService _notificationService;

        public AuthController(IAuthService authService,
            UserManager<User> userManager, IFileUploadService fileUploadService,
            IOTPService otpService, IEmailService emailService, INotificationService notificationService)
        {
            _authService = authService;
            _userManager = userManager;
            _fileUploadService = fileUploadService;
            _otpService = otpService;
            _emailService = emailService;
            _notificationService = notificationService;
        }

        [HttpPost("RegisterStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterStudent(StudentRegisterDto newUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ErrorOr<User> result = await _authService.RegisterStudent(newUser);
                    if (result.IsError) { return BadRequest(result.ErrorsOrEmptyList); }
                    else { return Created("Student Created", result.Value); }

                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return BadRequest("Please input all required filds");
        }

		[HttpPost("RegisterAdmin")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> RegisterAdmin([FromForm] AdminRegesterDTO New)
		{
			if (ModelState.IsValid)
			{
				try
				{
					ErrorOr<User> result = await _authService.RegisterAdmin(New);
					if (result.IsError) { return BadRequest(result.ErrorsOrEmptyList); }
					else { return Created("Admin Created", result.Value); }

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
        public async Task<IActionResult> RegisterTeacher(TeacherRegisterDto newTeacher)
        {
            if (ModelState.IsValid)
            {
                //var fileExtension = _fileUploadService.GetFileExtension(newTeacher.Certifications.FileName);
                //if (PhoneUploadFile.FileExtensions.Contains(fileExtension) is false)
                //{
                //    return BadRequest("Please Check File Type");
                //}
                try
                {
                    ErrorOr<User> result = await _authService.RegisterTeacher(newTeacher);
                    if (result.IsError) { return BadRequest(result.ErrorsOrEmptyList); }
                    else { return Created("Teacher Created", result.Value); }
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
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto applictionUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _authService.Login(applictionUser);
                    if (result.IsError) { return Unauthorized("The email or password is incorrect"); }
                    else { return Ok(result.Value); }
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

        [HttpPost("GenerateOtp")]
        [Authorize]
        public async Task<IActionResult> GenerateOtp(string email, int otpType)
        {
            bool IsEmailValid = _otpService.ValidateEmail(email);
            if(!IsEmailValid) { return BadRequest("Email is invalid"); }

            string userId = _userManager.GetUserId(HttpContext.User);
            User user = await _userManager.FindByIdAsync(userId);

            var code = await _otpService.GenerateOTP(userId, otpType);
            if (string.IsNullOrEmpty(code)) { return BadRequest("Faild To Crete The Code"); }

            _emailService.SendEmailAsync(email, code);
            return Ok("Email Is Sent");
        }

        [HttpPost("VerifyOTP")]
        public async Task<IActionResult> VerifyOTP(string otpCode)
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            bool IsOtpVerfied = await _otpService.VerifyOTP(otpCode, userId);

            if (IsOtpVerfied) { return Ok(IsOtpVerfied); }
            return BadRequest(IsOtpVerfied);

        }

		[HttpPost("SendNotfication")]
		public async Task<IActionResult> SendNotfication(string content)
		{
			string userId = _userManager.GetUserId(HttpContext.User);
			bool IsSent = await _notificationService.Send(userId,content);

			if (IsSent) { return Ok(IsSent); }
			return BadRequest(IsSent);

		}
	}

}
