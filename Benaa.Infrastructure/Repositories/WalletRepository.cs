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
        
       

        public async Task<int> GetAmountCode(string code)
        {
            
            var mc = await _dbContext.MoneyCodes.FirstOrDefaultAsync(s=>s.Code== code);
            int amount = mc.Amount;
            return amount;  


        }

        public async Task<decimal> AddAmountCode(string ui, int amount)
        {
            var user= await _dbContext.Users.FirstOrDefaultAsync(s => s.Id == ui);


            if (user.Wallet == null)
            {

                user.Wallet = new Wallet();
                await Create(user.Wallet);
            }
            string wi = user.WalletId.ToString();

            var wallet = await _dbContext.Wallets.FirstOrDefaultAsync(s => s.Id.ToString() == wi);
            wallet.Amount += amount;
            await _dbContext.SaveChangesAsync();
            decimal am = wallet.Amount;
            return am;
        
        }

        public async Task <decimal> check(string u)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(s => s.Id == u);
            if (user.WalletId == null) { return 0; }
            string w=user.WalletId.ToString(); 
            var wallet= await _dbContext.Wallets.FirstOrDefaultAsync(s => s.Id.ToString() == w);
            decimal amount = wallet.Amount;
            return amount;  
        }

    }
}
