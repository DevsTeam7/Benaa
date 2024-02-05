using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Infrastructure.Data;

namespace Benaa.Infrastructure.Repositories
{
    public class MoneyCodeRepository : BaseRepository<MoneyCode>, IMoneyCodeRepository
    {
        public MoneyCodeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
