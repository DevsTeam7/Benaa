using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Benaa.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public new async Task Update(User model)
        {
            _dbContext.Users.Update(model);
            _dbContext.Entry(model).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Wallet> GetUserWallet(string userId)
        {
          var wallet = await _dbContext.Users.Where(u => u.Id == userId)
                                .Select(user => _dbContext.Wallets.FirstOrDefault(userWallet => userWallet.Id == user.WalletId))
                                .Where(Wallet => Wallet != null).Select(Wallet => Wallet).FirstOrDefaultAsync();
            if(wallet == null) { throw new Exception(); }
            return wallet;
        }
    }
}
