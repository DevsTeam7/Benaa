using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Core.Interfaces.IServices;
//using Benaa.Infrastructure;

namespace Benaa.Core.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        public WalletService(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }
        public string IsPayed(decimal amount)
        {
            User user = new User();
            Wallet wallet = new Wallet();
            _walletRepository.GetById(user.Id);
            wallet.Amount = 1000000;


            if (wallet.Amount >= amount)
            {
                wallet.Amount -= amount;
                return "Done";
            }

            return "not done";

        }
    }
}
