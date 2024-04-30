using ErrorOr;
using Microsoft.AspNetCore.Http;

namespace Benaa.Core.Interfaces.IServices
{
    public interface IUserService
    {
        Task<ErrorOr<string>> UploadImage(IFormFile UserImage, string UserId);
    }
}