using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Infrastructure.Data;

namespace Benaa.Infrastructure.Repositories
{
    public class BankInformationRepository : BaseRepository<BankInformation>, IBankInformationRepository
    {
        public BankInformationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

    }
}
