using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Benaa.Infrastructure.Repositories
{
    public class ChatRepository : BaseRepository<Chat>, IChatRepository
    {
        public ChatRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<Chat> FirstAsync(Expression<Func<Chat, bool>> predicate)
        {
           return await _dbContext.Chats.FirstAsync(predicate);
        }
    }
}
