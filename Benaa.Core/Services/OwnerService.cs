﻿using AutoMapper;
using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Core.Interfaces.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benaa.Core.Services
{
    public class OwnerService: IOwnerService
    {
        private readonly IOwnerRepository _OwnerRepository;
        private readonly IPaymentRepositoty _paymentRepository;
        private readonly IMapper _mapper;
        public OwnerService(IOwnerRepository ownerRepository, IPaymentRepositoty paymentRepository, IMapper mapper)
        {
            _OwnerRepository = ownerRepository;
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }


        public async Task <List<User>> Get()
        {
            return await _OwnerRepository.GetNT();
        }

        public async Task del(string id)
        {
            var user = await _OwnerRepository.GetById(id);
            await _OwnerRepository.Delete(user);
        }

        public async Task ac(string id)
        {
            var user = await _OwnerRepository.GetById(id);
            user.IsApproved=true;
            await _OwnerRepository.SaveChangeAsync();
        }

        public async Task<List<User>> GetA()
        {
            return await _OwnerRepository.GetAD();
        }

        public async Task UpdateUser(UpdateUserInfo nu)
        {
            var us = await _OwnerRepository.GetById(nu.Id);

            us.IsApproved = nu.IsApproved;
            us.Email = nu.Email;
            //us.PasswordHash = nu.Password;

            await _OwnerRepository.Update(us);

            //User model = _mapper.Map<User>(nu);


            //await _OwnerRepository.Update(model);
        }

        public async Task<List<JoinPayment>> GetDues()
        {
            return await _OwnerRepository.GetD();
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
