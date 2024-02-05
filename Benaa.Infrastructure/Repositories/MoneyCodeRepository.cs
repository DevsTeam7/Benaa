using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benaa.Infrastructure.Repositories
{
    public class MoneyCodeRepository : BaseRepository<MoneyCode>, IMoneyCodeRepository
    {
        public MoneyCodeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
