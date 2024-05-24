using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Benaa.Core.Interfaces.IServices
{
    public interface IUserService
    {
        Task<ErrorOr<BankInformation>> AddBankInfo(CreateBankInfoDto bankInfoDto);
        Task<ErrorOr<string>> UploadImage(IFormFile file, string userId);
        Task<ErrorOr<IdentityResult>> Update(string userId, UserUpdateDto userUpdate);
        Task<ErrorOr<IdentityResult>> UpdatePassword(string userId, string newPassword, string? oldPassword = null);
        Task<ErrorOr<List<User>>> GetTeachers(int quantity);
        Task<User> Getuser(string id);
    }
}