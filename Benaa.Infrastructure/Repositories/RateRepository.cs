using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Benaa.Infrastructure.Repositories
{
    public class RateRepository : BaseRepository<Rate>, IRateRepository
    {
        public RateRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async new Task<List<Rate>> Select(Expression<Func<Rate, bool>> predicate)
        {
            var lsitOfItems = await _dbContext.Rates.Where(predicate).Include(user=> user.Student).ToListAsync();
            return lsitOfItems;
        }
    }
}
