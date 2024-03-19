using Benaa.Core.Entities.General;
using System.Linq.Expressions;
namespace Benaa.Core.Interfaces.IRepositories
{
    public interface IChatRepository : IBaseRepository<Chat>
    {
        Task<Chat> FirstAsync(Expression<Func<Chat, bool>> predicate);
    }
}
