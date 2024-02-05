using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Infrastructure.Data;

namespace Benaa.Infrastructure.Repositories
{
    public class SchedualRepository : BaseRepository<Sceduale>, ISchedualRepository
    {
        public SchedualRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
