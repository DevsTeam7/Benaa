using AutoMapper;
using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Core.Interfaces.IServices;
using ErrorOr;
using Microsoft.AspNetCore.Identity;

namespace Benaa.Core.Services
{
    public class OwnerService: IOwnerService
    {
        private readonly IOwnerRepository _OwnerRepository;
        private readonly IPaymentRepositoty _paymentRepository;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly UserManager<User> _userManager;

       
        public OwnerService(IOwnerRepository ownerRepository, IPaymentRepositoty paymentRepository,
            IMapper mapper, INotificationService notificationService, UserManager<User> userManager)
        {
            _OwnerRepository = ownerRepository;
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _notificationService = notificationService;
            _userManager = userManager;
        }


        public async Task <List<User>> Get()
        {
            return await _OwnerRepository.GetNT();
        }

        public async Task del(string id)
        {
            var user = await _OwnerRepository.GetById(id);
            if (user == null) { throw new Exception(); }
            await _OwnerRepository.Delete(user);
        }

        public async Task ac(string id)
        {
            var user = await _OwnerRepository.GetById(id);
            user.IsApproved=true;
            await _OwnerRepository.Update(user);
            await _notificationService.Send(user.Id,"تهانينا لقد تم قبولك في منصة بناء");
        }


        public async Task<List<User>> GetA()
        {
            return await _OwnerRepository.GetAD();
        }

		
		public async Task UpdateUser(UpdateUserInfo nu)
        {
            var us = await _OwnerRepository.GetById(nu.Id);
            await _userManager.RemovePasswordAsync(us);
           var result = await _userManager.AddPasswordAsync(us, nu.Password);

            // us.IsApproved = nu.IsApproved;
            us.Email = nu.Email;
            us.FirstName = nu.FirstName;
            //us.PasswordHash = nu.Password;

            await _OwnerRepository.Update(us);

            //User model = _mapper.Map<User>(nu);


            //await _OwnerRepository.Update(model);
        }

        public async Task<List<JoinPayment>> GetDues()
        {
            return await _OwnerRepository.GetD();
        }

        public async Task<List<JoinPayment>> GetPaid()
        {
            return await _OwnerRepository.GetP();
        }

        public async Task delpay(Guid id)
        {
            var pay = await _paymentRepository.GetById(id);
            pay.Status = 2;
            await _paymentRepository.SaveChangeAsync();
        }

        public async Task<IncomsInfo> getInfo()
        {
            return await _OwnerRepository.GetINFO();
        }


        //////////////////////////////////////
        ///



    }
}
