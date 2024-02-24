using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NpgsqlTypes;


//using Benaa.Infrastructure;

namespace Benaa.Core.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IPaymentRepositoty _paymentRepository;

        public WalletService(IWalletRepository walletRepository, IPaymentRepositoty paymentRepository, UserManager<User> userManager)
        {
            _walletRepository = walletRepository;
            _paymentRepository = paymentRepository;
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

        public async Task<object>SetPayment(Guid itemID, string type, decimal price)
        {
            if (type == "schedual")
            {
                string Techerid = await _walletRepository.getTecherid(itemID);
                string Studentid = await _walletRepository.getStudentid(itemID);
                Payment payment= new Payment();
                payment.Type = type;
                payment.Amount= price;
                payment.ItemId= itemID;
                payment.TeacherId= Techerid;
                payment.StudentId = Studentid;
                await _paymentRepository.Create(payment);
                return payment;
            }
            return false;   
        }

    }
}
