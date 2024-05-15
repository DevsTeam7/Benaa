using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Infrastructure.Data;
using Benaa.Infrastructure.Utils.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Benaa.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly UserManager<User> _userManager;
        public UserRepository(ApplicationDbContext dbContext, UserManager<User> userManager) : base(dbContext)
        {
            _userManager = userManager;
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

        public async Task<List<User>> SelectQuantity(int quantity)
        {
            var usersInRole = await _userManager.GetUsersInRoleAsync(Role.Teacher);

            var selectedUsers = usersInRole.Take(quantity);

            foreach (User user in selectedUsers)
            {
                List<Sceduale> sceduale = await _dbContext.Sceduales
                    .Where(s => s.TeacherId == user.Id)
                    .ToListAsync();

                user.Sceduales = sceduale;
            }

            return selectedUsers.ToList();
        }
    }
}
