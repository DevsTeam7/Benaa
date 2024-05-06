using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Core.Interfaces.IServices;
using ErrorOr;
using System.Text.RegularExpressions;
namespace Benaa.Infrastructure.Services
{
    public class OTPService : IOTPService
    {
        private readonly IOTPCodesRepository _otpCodesRepository;
        public OTPService(IOTPCodesRepository oTPCodesRepository)
        {
            _otpCodesRepository = oTPCodesRepository;
        }
        public async Task<string> GenerateOTP(string userId, int type)
        {
            OtpType otpType; 
            if(type == 0)
            {
                otpType = OtpType.Confirmation;
            }
            otpType = OtpType.ResetPassword;

            int randomNumber = new Random().Next(1000, 9999);
            var userCode = await _otpCodesRepository.CreateOTP(userId, otpType, randomNumber);
            if (userCode == null) {return string.Empty; }
            else { return userCode.Code; }
            //Send Code to user

        }
        public bool ValidateEmail(string email)
        {
            string pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
            bool isValid = Regex.IsMatch(email, pattern);
            return isValid;
        }
        public async Task<bool> VerifyOTP(string otp, string userId)
        {
            if (await _otpCodesRepository.VerifyCode(otp, userId)) return true;
            return false;
        }
    }


}
