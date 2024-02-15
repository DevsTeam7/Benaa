using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Infrastructure.Data;

namespace Benaa.Infrastructure.Repositories
{
    public class SchedualRepository : BaseRepository<Sceduale>, ISchedualRepository
    {
        protected readonly ApplicationDbContext _dbContext;

        public SchedualRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

      

    }
}
