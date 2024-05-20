using Benaa.Core.Entities.General;
using ErrorOr;

namespace Benaa.Core.Interfaces.IServices
{
    public interface IMoneyCodeService
    {
        Task<ErrorOr<List<MoneyCode>>> Generate(int amount, int number);
    }
}
