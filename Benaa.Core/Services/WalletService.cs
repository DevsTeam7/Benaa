using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Core.Interfaces.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Benaa.Infrastructure;

namespace Benaa.Core.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        public WalletService(IWalletRepository walletRepository) {
            _walletRepository = walletRepository;
        }  
        public string IsPayed(decimal amount)
        {
            User user = new User();
            Course wallet = new Course();
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
