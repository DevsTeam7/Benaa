using AutoMapper;
using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace Benaa.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;


        public AuthService(UserManager<User> userManager, IConfiguration config,
            RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
            _config = config;
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

        public async Task<User?> Registration(RegisterRequestDto newUser)
        {
            if (await IsUserExist(newUser))
                return null;

            User user = await CreateUser(newUser);
            if (user != null)
            {
                if (await IsRoleExist(newUser))
                    await _userManager.AddToRoleAsync(user, newUser.Role);
                return (user);
            }
            return null;
        }

        public async Task<string> Login(LoginRequestDto applictionUser)
        {
            var user = await _userManager.FindByEmailAsync(applictionUser.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, applictionUser.Passwrod))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var token = GenerateTokenString(user, userRoles);
                return (token);
            }
            return string.Empty;
        }

        public string GenerateTokenString(User user, IList<string> userRoles)
        {
            IList<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));
            SigningCredentials signinCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(3),
                issuer: _config.GetSection("Jwt:Audience").Value,
                audience: _config.GetSection("Jwt:Issuer").Value,
                signingCredentials: signinCred
                );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return tokenString;
        }
    }
}
