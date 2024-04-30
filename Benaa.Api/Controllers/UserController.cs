using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IServices;
using Benaa.Core.Utils.FileUploadTypes;
using Benaa.Infrastructure.Services;
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
                    return Ok(result);
  

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            };
        }

      //TODO: ALL USER Settings done here  
    }
}
