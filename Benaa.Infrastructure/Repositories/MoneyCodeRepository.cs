using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Benaa.Infrastructure.Repositories
{
    public class MoneyCodeRepository : BaseRepository<MoneyCode>, IMoneyCodeRepository
    {
        public MoneyCodeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
           
        }

        public async Task<List<string>> GetAllCode()
        {
            List<string> codes = await _dbContext.MoneyCodes.Select(e => e.Code).ToListAsync();
            return codes;
        }
    }
}
