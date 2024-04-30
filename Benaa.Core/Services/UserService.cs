using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using Benaa.Core.Entities.DTOs;
using Microsoft.AspNetCore.Http;

namespace Benaa.Core.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IFileUploadService _fileUploadService;
        public UserService(UserManager<User> userManager,
            IFileUploadService fileUploadService)
        {
            _userManager = userManager;
            _fileUploadService = fileUploadService;
        }

        [HttpPost("UploadImage")]
        public async Task<ErrorOr<string>> UploadImage(IFormFile UserImage, string UserId)
        {
            User user = await _userManager.FindByIdAsync(UserId);

            if (user is null) { return Error.NotFound("The User Does not Exist"); }

            string imagePath = await _fileUploadService.UploadFile(UserImage);
            if (string.IsNullOrEmpty(imagePath)) { return Error.Failure("Faild To Upload The Image"); }

            user.ImageUrl = imagePath;
           var res = await _userManager.UpdateAsync(user);
            if(!res.Succeeded) { return Error.Unexpected("Falid To Update The User"); }
            return imagePath ;
        }
    }
}
