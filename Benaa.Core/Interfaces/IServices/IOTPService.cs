using Benaa.Core.Entities.General;
using ErrorOr;

namespace Benaa.Core.Interfaces.IServices
{
    public interface IOTPService
    {
        Task<string> GenerateOTP(string userId, int type);
        Task<bool> VerifyOTP(string otp, string userId);
    }
}