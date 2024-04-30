using Benaa.Core.Entities.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benaa.Core.Interfaces.IRepositories
{
    public interface IOTPCodesRepository : IBaseRepository<OTPCodes>
    {
        Task<OTPCodes> CreateOTP(string userId, OtpType type, int randomNumber);
        Task<bool> VerifyCode(string code, string userId);
    }
}
