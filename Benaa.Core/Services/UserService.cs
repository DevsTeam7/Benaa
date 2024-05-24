﻿using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Identity;
using ErrorOr;
using Benaa.Core.Entities.DTOs;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Benaa.Core.Interfaces.IRepositories;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Benaa.Core.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IFileUploadService _fileUploadService;
        private readonly IMapper _mapper;
        private readonly IBankInformationRepository _bankInformationRepository;
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;



        public UserService(UserManager<User> userManager, IUserRepository userRepository,
            IFileUploadService fileUploadService, IMapper mapper,
            IBankInformationRepository bankInformationRepository, ILogger<UserService> logger)
        {
            _userManager = userManager;
            _fileUploadService = fileUploadService;
            _mapper = mapper;
            _bankInformationRepository = bankInformationRepository;
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<string>> UploadImage(IFormFile file, string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);

            if (user is null) { return Error.NotFound("The User Does not Exist"); }

            string filePath = await _fileUploadService.UploadFile(file);
            if (string.IsNullOrEmpty(filePath)) { return Error.Failure("Faild To Upload The Image"); }

            user.ImageUrl = filePath;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) { return Error.Unexpected("Falid To Update The User"); }
            return filePath;
        }

        public async Task<ErrorOr<BankInformation>> AddBankInfo(CreateBankInfoDto bankInfoDto, string userId)
        {
            var user = await _userManager.Users.FirstAsync(user => user.Id == userId);
            BankInformation bankInfo = _mapper.Map<BankInformation>(bankInfoDto);
            var createdbankInfo = await _bankInformationRepository.Create(bankInfo);
            user.BankInformationId = createdbankInfo.Id;
			await _userManager.UpdateAsync(user);
            if (createdbankInfo is null) { return Error.Failure(); }
            return createdbankInfo;
        }
        //Its not working
        public async Task<ErrorOr<IdentityResult>> Update(string userId, UserUpdateDto userUpdate)
        {
            User user = await _userManager.Users.Where(user => user.Id == userId).FirstOrDefaultAsync();
            if (user is null) { return Error.NotFound("The User Does not Exist"); }

            if (string.IsNullOrEmpty(userUpdate.Email))
            {
                userUpdate.Email = user.Email;
                userUpdate.UserName = user.Email;
            }

            userUpdate.City ??= user.City;
            userUpdate.FirstName ??= user.FirstName;
            userUpdate.LastName ??= user.LastName;
            userUpdate.EducationLevel ??= user.EducationLevel;
            userUpdate.Specialization ??= user.Specialization;
            userUpdate.Experience ??= user.Experience;
            userUpdate.University ??= user.University;
            userUpdate.City ??= user.City;
            userUpdate.DateOfBirth ??= user.DateOfBirth;
            userUpdate.Gender ??= user.Gender;
            userUpdate.PhoneNumber ??= user.PhoneNumber;
            //var mappedUser = _mapper.Map<User>(userUpdate);

            IdentityResult result = await _userManager.UpdateAsync(user);
            await _userRepository.Update(user);


            //foreach (var item in result.Errors.ToList())
            //{
            //    _logger.LogError(item.Code);
            //    _logger.LogError(item.Description);
            //}
            if (!result.Succeeded) { return Error.Unexpected("Faild To Update The User " + user.Id); }
            //await _userRepository.SaveChangeAsync();
            return result;
        }

        public async Task<ErrorOr<IdentityResult>> UpdatePassword(string userId, string newPassword, string? oldPassword = null)
        {
            User user = await _userManager.FindByIdAsync(userId)!;
            if (user is null) { return Error.NotFound("The User Does not Exist"); }
            IdentityResult result;
            if (oldPassword != null)
            {
                result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            }
            await _userManager.RemovePasswordAsync(user);
            result = await _userManager.AddPasswordAsync(user, newPassword);

            if (!result.Succeeded) { return Error.Unexpected("Falid To Update The User"); }
            return result;

        }
        //TODO: conecte and test the scudule logic
        public async Task<ErrorOr<List<User>>> GetTeachers(int quantity)
        {
           var teachers = await _userRepository.SelectQuantity(quantity); 
            if(teachers == null) { return Error.NotFound(); }
            return teachers;
        }

        public async Task<ErrorOr<Success>> Delete(string userId)
        {
            var user = _userManager.Users.First(user => user.Id == userId);
            if (user is null) { return Error.NotFound(); };
            IdentityResult result = await _userManager.DeleteAsync(user);
            if(!result.Succeeded) { return Error.Failure();}
            return new Success();
        }
        public async Task<User> Getuser(string id)
        {
            var user = await _userRepository.GetById(id);
            return user;
        }
    }
}
