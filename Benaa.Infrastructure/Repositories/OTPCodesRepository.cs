﻿using Benaa.Core.Entities.General;
using Benaa.Core.Exceptions;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Benaa.Infrastructure.Repositories
{
    public class OTPCodesRepository : BaseRepository<OTPCodes>, IOTPCodesRepository
    {
        public OTPCodesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<OTPCodes> CreateOTP(string userId, OtpType type, int randomNumber)
        {
            OTPCodes newOtp = new OTPCodes { UserId = userId, Type = type, Code = randomNumber.ToString() };
            return await Create(newOtp);
        }
        public async Task<bool> VerifyCode(string code, string userId)
        {
            var Code = await _dbContext.OTPCodes.FirstOrDefaultAsync(user => user.UserId == userId && user.Code == code);
            if (Code == null) throw new NotFoundException() ;
            return true;
        }
    }
}
