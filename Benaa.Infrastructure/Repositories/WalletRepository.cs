using Benaa.Core.Entities.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Infrastructure.Data;

namespace Benaa.Infrastructure.Repositories
{
    public class WalletRepository : BaseRepository<Wallet>, IWalletRepository
    {
        public WalletRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
