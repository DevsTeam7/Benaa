using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Infrastructure.Data;
namespace Benaa.Infrastructure.Repositories
{
    public class MessageRepository : BaseRepository<Messages>, IMessageRepository
    {
        public MessageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
