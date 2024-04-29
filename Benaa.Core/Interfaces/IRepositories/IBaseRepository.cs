using System.Linq.Expressions;

namespace Benaa.Core.Interfaces.IRepositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById<Tid>(Tid id);
        Task<T> Create(T model);
        Task CreateRange(List<T> model);
        Task Update(T model);
        Task Delete(T model);
        Task SaveChangeAsync();
        Task<List<T>> Select(Expression<Func<T, bool>> predicate);
        Task<T> SelectOneItem(Expression<Func<T, bool>> predicate)
    }
}
