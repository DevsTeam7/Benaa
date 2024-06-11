using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Benaa.Infrastructure.Repositories
{
    public class WalletRepository : BaseRepository<Wallet>, IWalletRepository
    {
        public WalletRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        
       

        public async Task<MoneyCode> GetCode(string code)
        {
            
            var moneyCode = await _dbContext.MoneyCodes.FirstOrDefaultAsync(s=>s.Code== code && s.Status == false);
            return moneyCode;  


        }

        public async Task<decimal> AddAmountCode(string ui, decimal amount)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(s => s.Id == ui);

            string wi = user.WalletId.ToString();

            var wallet = await _dbContext.Wallets.FirstOrDefaultAsync(s => s.Id.ToString() == wi);
            wallet.Amount += amount;
            await SaveChangeAsync();
            decimal am = (decimal)wallet.Amount;
            return am;
        }

        public async Task<decimal> check(string u)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(s => s.Id == u);
            //if (user.WalletId == null) { return 0; }
            //Guid w = user.WalletId;
            var wallet = await _dbContext.Wallets.FirstOrDefaultAsync(s => s.Id == user.WalletId);
			decimal amount = wallet.Amount;
            return amount;
        }

        public async Task<string> getTecherid(Guid itemID)
        {
            var sc= await _dbContext.Sceduales.FirstOrDefaultAsync(s => s.Id == itemID);
            string id = sc.TeacherId;
            return id;
        }

        public async Task<string> getStudentid(Guid itemID)
        {
            var sc = await _dbContext.Sceduales.FirstOrDefaultAsync(s => s.Id == itemID);
            string id = sc.StudentId;
            return id;
        }

	}
}
