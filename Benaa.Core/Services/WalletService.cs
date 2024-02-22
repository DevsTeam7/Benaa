using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


//using Benaa.Infrastructure;

namespace Benaa.Core.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        
        public WalletService(IWalletRepository walletRepository, UserManager<User> userManager)
        {
            _walletRepository = walletRepository;
            
        }


       
        public async Task< decimal> ChargeWallet(string ui, string code)
        {
            Task<int>task = _walletRepository.GetAmountCode(code);
            int amount = await task;

            Task<decimal>tasks= _walletRepository.AddAmountCode(ui, amount);
            decimal result = await tasks;


            return result;
        }
        //public string IsPayed(decimal amount)
        //{
        //    User user = new User();
        //    Wallet wallet = new Wallet();
        //    _walletRepository.GetById(user.Id);
        //    wallet.Amount = 1000000;


        //    if (wallet.Amount >= amount)
        //    {
        //        wallet.Amount -= amount;
        //        return "Done";
        //    }

        //    return "not done";

        //}


        public async Task<bool> Check(string u, int price)
        {
            Task<decimal> task = _walletRepository.check(u);
            decimal p= await task;
            if(p>= price) { return true; }

            return false;
        }

    }
}
