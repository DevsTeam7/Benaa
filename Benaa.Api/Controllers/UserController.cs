using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IServices;
using Benaa.Core.Utils.FileUploadTypes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Benaa.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly IFileUploadService _fileUploadService;
        public UserController(IUserService userService,
            UserManager<User> userManager,
            IFileUploadService fileUploadService)
        {
            _userService = userService;
            _userManager = userManager;
            _fileUploadService = fileUploadService;
        }

        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile UserImage)
        {
            var fileExtension = _fileUploadService.GetFileExtension(UserImage.FileName);
            if (PhoneUploadFile.ImageExtensions.Contains(fileExtension) is false)
            {
                return BadRequest("Please Check File Type");
            }
            try
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                var result = await _userService.UploadImage(UserImage, userId);
                if (result.IsError) { return BadRequest(result.Errors); }
                return Ok(result.Value);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            };
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromForm] UserUpdateDto userUpdateDto)
        {
            try {
                if (ModelState.IsValid)
                {
                    var userId = _userManager.GetUserId(HttpContext.User);

                    var result = await _userService.Update(userId, userUpdateDto);

                    if (result.IsError) { return BadRequest(result.ErrorsOrEmptyList); }
                    return Ok(result.Value);
                }
                return BadRequest("Please input all required filds");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            };

        }

        [HttpPut("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(string newPassword, string? oldPassword = null)
        {
            try
            {
                var userId = _userManager.GetUserId(HttpContext.User);

                var result = await _userService.UpdatePassword(userId, newPassword, oldPassword);

                if (result.IsError) { return BadRequest(result.ErrorsOrEmptyList); }
                return Ok(result.Value);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            };
        }

        [HttpGet("GetTeachers")]
        public async Task<IActionResult> GetTeachers(int quantity)
        {
            try
            {
                var result = await _userService.GetTeachers(quantity);
                if (result.IsError) { return BadRequest(result.ErrorsOrEmptyList); }
                return Ok(result.Value);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            };
        }

        [HttpPost("AddBankInfo")]
        public async Task<IActionResult> AddBankInfo(CreateBankInfoDto bankInfoDto)
        {
            try{
                if (ModelState.IsValid)
                {
                    var userId = _userManager.GetUserId(HttpContext.User);
                    var result = await _userService.AddBankInfo(bankInfoDto, userId!);
                    if (result.IsError) { return BadRequest(result.ErrorsOrEmptyList); }
                    return Ok(result.Value);
                }
                return BadRequest("Please input all required filds");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            };

		}

	}
}
