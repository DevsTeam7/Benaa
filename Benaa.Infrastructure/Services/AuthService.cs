using AutoMapper;
using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.Authentication;
using Benaa.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Identity;
using ErrorOr;
using Benaa.Infrastructure.Utils.Users;
using Microsoft.AspNetCore.Http;


namespace Benaa.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenGeneration _tokenGeneration;
        private readonly IFileUploadService _fileUploadService;
        private readonly IWalletService _walletService;
        private readonly IOTPService _otpService;
        private readonly IEmailService _emailService;
        public AuthService(UserManager<User> userManager, IMapper mapper
            , ITokenGeneration tokenGeneration,
            IFileUploadService fileUploadService,
            IWalletService walletService,
            IEmailService emailService,
            IOTPService oTPService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenGeneration = tokenGeneration;
            _fileUploadService = fileUploadService;
            _walletService = walletService;
            _otpService = oTPService;
            _emailService = emailService;
        }

        private async Task<bool> IsUserExist(User newUser)
        {
            var userExists = await _userManager.FindByEmailAsync(newUser.Email);
            if (userExists != null) return true;
            return false;
        }

        private async Task<User> CreateUser(User newUser, string Password, IFormFile? file = null)
        {
            
            if (file is not null && file.Length > 0)
            {
                var UploadedFile = await _fileUploadService.UploadFile(file);
                if (!string.IsNullOrEmpty(UploadedFile))
                    newUser.CertificationUrl = UploadedFile;
            }

            newUser.UserName = newUser.Email;

            var createUserResult = await _userManager.CreateAsync(newUser, Password);

            if (!createUserResult.Succeeded)
                return null;

            return newUser;
        }

        public async Task<ErrorOr<User>> RegisterStudent(StudentRegisterDto newStudent)
        {
            User newUser = _mapper.Map<User>(newStudent);
            if (await IsUserExist(newUser))
                return Error.Conflict(description: "The Email Exist");

            User user = await CreateUser(newUser,newStudent.Password);
            if (user == null) { return Error.Unexpected("Faild To Create the user"); }
            Guid walletId = await _walletService.CraeteWallet();
            user.WalletId = walletId;

            if (user is null)
            {
                return Error.Failure(description: "Faild To Create the Account");
            }
            await _userManager.AddToRoleAsync(user, Role.Student);

            return user;
        }

        public async Task<ErrorOr<LoginRequestDto.Response>> Login(LoginRequestDto.Request applictionUser)
        {
            var user = await _userManager.FindByEmailAsync(applictionUser.Email);
            if (user is null)
                return Error.NotFound(description: "The Email Does not Exist");

            bool IsPassWordCorrect = await _userManager.CheckPasswordAsync(user, applictionUser.Password);
            if (IsPassWordCorrect)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var token = _tokenGeneration.GenerateTokenString(user, userRoles);

                LoginRequestDto.Response? authenticatedUser = _mapper.Map<LoginRequestDto.Response>(user);

                if (authenticatedUser is null) return Error.Unexpected();
                if (user.EmailConfirmed is false) { authenticatedUser.EmailConfirmed = user.EmailConfirmed; }
                authenticatedUser.Token = token;
                authenticatedUser.ImageUrl = user.ImageUrl;

                return authenticatedUser;
            }
            return Error.Unauthorized(description: "The Password Is Wrong!");
        }


        public async Task<ErrorOr<User>> RegisterTeacher(TeacherRegisterDto newTeacher)
        {
            User newUser = _mapper.Map<User>(newTeacher);
            if (await IsUserExist(newUser))
                return Error.Conflict(description: "The Email Exist");

            User user = await CreateUser(newUser, newTeacher.Password, newTeacher.Certifications);

            if (user is null)
            {
                return Error.Failure(description: "Faild To Create the Account");
            }
            await _userManager.AddToRoleAsync(user, Role.Teacher);

            return user;
        }

    }
}
