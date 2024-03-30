using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
namespace Benaa.Infrastructure.Services
{
    public class OTPService //: IOTPService
    {
        private readonly IOTPCodesRepository _otpCodesRepository;
        public OTPService(IOTPCodesRepository oTPCodesRepository) {
            _otpCodesRepository = oTPCodesRepository;
        }
        public async void GenerateOTP(string userId, OtpType type)
        {
            int randomNumber = new Random().Next(1000, 9999);
            var userCode = await _otpCodesRepository.CreateOTP(userId, type, randomNumber);
            //Send Code to user

        }
        public async Task<bool> VerifyOTP(string otp, string userId)
        {
           if(await _otpCodesRepository.VerifyCode(otp , userId)) return true;
           return false;
        }
    }


}
