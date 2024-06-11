using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Benaa.Core.Interfaces.IServices
{
    public interface IUserService
    {
        Task<ErrorOr<BankInformation>> AddBankInfo(CreateBankInfoDto bankInfoDto, string userId);
        Task<ErrorOr<Success>> Upload(string userId, IFormFile? image = null, IFormFile? certification = null);
		Task<ErrorOr<IdentityResult>> Update(string userId, UserUpdateDto userUpdate);
        Task<ErrorOr<IdentityResult>> UpdatePassword(string userId, string newPassword, string? oldPassword = null);
        Task<ErrorOr<Success>> Delete(string userId);
        Task<ErrorOr<List<User>>> GetTeachers();
        Task<ErrorOr<LoginResponseDto>> GetUserInfo(string userId);
	}
}