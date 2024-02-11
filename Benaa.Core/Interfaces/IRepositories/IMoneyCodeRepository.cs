using Benaa.Core.Entities.General;

namespace Benaa.Core.Interfaces.IRepositories
{
    public interface IMoneyCodeRepository : IBaseRepository<MoneyCode>
    {
        Task<List<string>> GetAllCode();
    }
}
