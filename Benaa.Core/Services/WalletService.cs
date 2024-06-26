﻿using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Core.Interfaces.IServices;
using ErrorOr;

namespace Benaa.Core.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IPaymentRepositoty _paymentRepository;
        private readonly ISchedualeRepository _schedualRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMoneyCodeRepository _moneyCodeRepository;


        public WalletService(IWalletRepository walletRepository, IPaymentRepositoty paymentRepository,
             ISchedualeRepository schedualRepository, ICourseRepository courseRepository,
           IUserRepository userRepository,IMoneyCodeRepository moneyCodeRepository)
        {
            _walletRepository = walletRepository;
            _paymentRepository = paymentRepository;
            _schedualRepository = schedualRepository;
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _moneyCodeRepository = moneyCodeRepository;
        }
        public async Task<Guid> CraeteWallet()
        {
            Wallet wallet = new Wallet();
            var createdWallet = await _walletRepository.Create(wallet);
            return createdWallet.Id;
        }

        public async Task<ErrorOr<decimal>> ChargeWallet(string userId, string code)
        {
            var moneyCode = await _moneyCodeRepository.SelectOneItem(c=> c.Code == code && c.Status == false );
            if(moneyCode == null ) { return Error.Unexpected("the code is used"); }

            var user = await _userRepository.SelectOneItem(user=> user.Id == userId);
			if (user == null) { return Error.NotFound("the user does not exist"); }

			var wallet = await _walletRepository.SelectOneItem(wallet => wallet.Id == user.WalletId);
			if (wallet == null) { return Error.NotFound("the wallet does not exist"); }

			wallet.Amount += moneyCode.Amount;
			await _walletRepository.Update(wallet);

            moneyCode.Status = true;
            await _moneyCodeRepository.Update(moneyCode);

            return wallet.Amount;
        }

        public async Task<bool> Check(string u, decimal price)
        {
            Task<decimal> task = _walletRepository.check(u);
            decimal p = await task;
            if (p >= price) { return true; }

            return false;
        }

        public async Task<object>SetPayment(Guid itemID, string type, decimal price,string ui)
        {
            Payment payment = new Payment();
            if (type == "schedual")
            {
               var schedual = await _schedualRepository.SelectOneItem(schedual => schedual.Id == itemID);
                if (schedual == null) { return false; }
                payment.TeacherId = schedual.TeacherId;
            }
            else if(type == "course")
            {
                var course = await _courseRepository.SelectOneItem(course => course.Id == itemID);
                if (course == null){return false; }
                payment.TeacherId = course.TeacherId!;
            }

            payment.Type = type;
            payment.Amount = price;
            payment.ItemId = itemID;
            payment.StudentId = ui;

            await _paymentRepository.Create(payment);
			var wallet = await _userRepository.GetUserWallet(ui);
            wallet.Amount -= price;
            await _walletRepository.Update(wallet);
			return payment;
  
        }

        public async Task RefundUser(decimal amount, string studentId)
        {
          var wallet = await  _userRepository.GetUserWallet(studentId);
            if(wallet == null) { throw new Exception(); }
            wallet.Amount += amount;
            await _walletRepository.Update(wallet);
        }

        public async Task<ErrorOr<decimal>> GetUserAmount(string userId)
        {
			var user = await _userRepository.SelectOneItem(user => user.Id == userId);
			if (user == null) { return Error.NotFound("the user does not exist"); }

			var wallet = await _walletRepository.SelectOneItem(wallet => wallet.Id == user.WalletId);
			if (wallet == null) { return Error.NotFound("the wallet does not exist"); }
            return wallet.Amount;
		}

    }
}
