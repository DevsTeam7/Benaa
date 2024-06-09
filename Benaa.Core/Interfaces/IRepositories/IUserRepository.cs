using Benaa.Core.Entities.General;

namespace Benaa.Core.Interfaces.IRepositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<Wallet> GetUserWallet(string userId);
        Task<List<User>> SelectQuantity(int quantity);
        new Task<List<User>> GetAll();

	}
}
