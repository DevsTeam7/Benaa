using Benaa.Core.Entities.General;

namespace Benaa.Core.Interfaces.IRepositories
{
    public interface IWalletRepository : IBaseRepository<Wallet>
    {
        Task<decimal> check(string u);

        Task<string> getTecherid(Guid itemID);
        Task<string> getStudentid(Guid itemID);

    }
}
