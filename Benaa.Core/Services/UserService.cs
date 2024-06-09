using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Identity;
using ErrorOr;
using Benaa.Core.Entities.DTOs;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Benaa.Core.Interfaces.IRepositories;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static System.Net.Mime.MediaTypeNames;

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

        public async Task<ErrorOr<Success>> Upload(string userId, IFormFile? image = null, IFormFile? certification = null)
        {
            User user = await _userManager.FindByIdAsync(userId);

            if (user is null) { return Error.NotFound("The User Does not Exist"); }
            if(image != null)
            {
				string imagePath = await _fileUploadService.UploadFile(image);
				if (string.IsNullOrEmpty(imagePath)) { return Error.Failure("Faild To Upload The Image"); }
				user.ImageUrl = imagePath;
			}
			if (certification != null)
			{
				string certificationPath = await _fileUploadService.UploadFile(certification);
				if (string.IsNullOrEmpty(certificationPath)) { return Error.Failure("Faild To Upload The Image"); }
				user.ImageUrl = certificationPath;
			}




            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) { return Error.Unexpected("Falid To Update The User"); }

            return new Success();
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

        public async Task<ErrorOr<IdentityResult>> Update(string userId, UserUpdateDto updaatedUser)
        {
            User user = await _userManager.Users.Where(user => user.Id == userId).FirstOrDefaultAsync();
            if (user is null) { return Error.NotFound("The User Does not Exist"); }
            if (string.IsNullOrEmpty(updaatedUser.Email))
            {
				updaatedUser.Email = user.Email;
				updaatedUser.UserName = user.Email;
            }
			updaatedUser.UserName = updaatedUser.UserName;
			var mappedUser = _mapper.Map(updaatedUser, user);

            if (updaatedUser.ImageUrl != null)
            {
				string imagePath = await _fileUploadService.UploadFile(updaatedUser.ImageUrl);
				if (string.IsNullOrEmpty(imagePath)) { return Error.Failure("Faild To Upload The Image"); }
				user.ImageUrl = imagePath;
			}
            if (updaatedUser.CertificationUrl != null){
				string certificationPath = await _fileUploadService.UploadFile(updaatedUser.CertificationUrl);
				if (string.IsNullOrEmpty(certificationPath)) { return Error.Failure("Faild To Upload The certification"); }
				user.CertificationUrl = certificationPath;
			}


            IdentityResult result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) { return Error.Unexpected("Faild To Update The User " + user.Id); }
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
            user.City = newPassword;
			await _userRepository.Update (user);

            if (!result.Succeeded) { return Error.Unexpected("Falid To Update The User"); }
            return result;

        }
        public async Task<ErrorOr<List<User>>> GetTeachers()
        {
           var teachers = await _userRepository.GetAll(); 
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
