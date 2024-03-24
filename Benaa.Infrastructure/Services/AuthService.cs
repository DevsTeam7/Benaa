using AutoMapper;
using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.Authentication;
using Benaa.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ErrorOr;


namespace Benaa.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly ITokenGeneration _tokenGeneration;


        public AuthService(UserManager<User> userManager, IConfiguration config,
            RoleManager<IdentityRole> roleManager, IMapper mapper
            , ITokenGeneration tokenGeneration)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
            _config = config;
            _tokenGeneration = tokenGeneration;
        }

        private async Task<bool> IsUserExist(RegisterRequestDto newUser)
        {
            var userExists = await _userManager.FindByEmailAsync(newUser.Email);
            if (userExists != null) return true;
            return false;
        }

        private async Task<User> CreateUser(RegisterRequestDto newUser)
        {
            User user = _mapper.Map<User>(newUser);
            user.UserName = newUser.Email;

            var createUserResult = await _userManager.CreateAsync(user, newUser.Password);
            if (!createUserResult.Succeeded)
                return null;
            return user;
        }

        private async Task<bool> IsRoleExist(RegisterRequestDto newUser)
        {
            if (await _roleManager.RoleExistsAsync(newUser.Role)) return true;
            return false;

        }

        public async Task<ErrorOr<User?>> Registration(RegisterRequestDto newUser)
        {
            if (await IsUserExist(newUser))
                return Error.Conflict(description: "The Email Exist");

            User user = await CreateUser(newUser);
            if (user is not null)
            {
                if (await IsRoleExist(newUser))
                    await _userManager.AddToRoleAsync(user, newUser.Role);
                return (user);
            }
            return Error.Failure(description: "Faild To Create the Account");
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

                authenticatedUser.Token = token;

                return authenticatedUser;
            }
            return Error.Validation(description: "The Password Is Wrong!");
        }

    }
}
