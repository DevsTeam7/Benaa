using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IServices;
using Benaa.Core.Services;
using Benaa.Core.Utils.FileUploadTypes;
using Benaa.Infrastructure.Services;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace Benaa.Api.Controllers
{
    [Route("api/User")]
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
                if (User.Identity!.IsAuthenticated)
                {
                    var userId = _userManager.GetUserId(HttpContext.User);
                    var result = await _userService.UploadImage(UserImage, userId);
                    if (result.IsError) { return BadRequest(result.Errors); }
                    return Ok(result);
                }
                else return Unauthorized();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            };
        }

        //TODO: ALL USER Settings done here  

    }
}
