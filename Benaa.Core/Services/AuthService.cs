using Benaa.Core.Entities.General;
using Microsoft.AspNetCore.Identity;
using Benaa.Core.Interfaces.IServices;
using Benaa.Core.Entities.DTOs;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Benaa.Core.Services
{
    public class AuthService : IAuthService
    {
        public readonly UserManager<User> _userManager;
        public readonly IConfiguration _config;
    

        public AuthService(UserManager<User> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        public async Task<bool> Login(RegisterRequestDto user)
        {
            var _user = await _userManager.FindByEmailAsync(user.Email);
            if (_user is null) {
                return false;
            }
            return await _userManager.CheckPasswordAsync(_user, user.Passwrod);
        }

        public async Task<IEnumerable<IdentityError>> RegisterUser(RegisterRequestDto _user)
        {
            
            User user = new User();
   
            user.Email = _user.Email;
            user.UserName = _user.UserName;
            var result = await _userManager.CreateAsync(user,_user.Passwrod);
            return result.Errors;
        }

        public string GenerateTokenString(RegisterRequestDto user)
        {
            IEnumerable<System.Security.Claims.Claim> claims = new List<Claim> { 
                new Claim(ClaimTypes.Email, user.Email),
                //serach for the role
                new Claim(ClaimTypes.Role, "Admin")
                };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value!));
            SigningCredentials signinCred = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha512Signature);
            JwtSecurityToken securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                issuer: _config.GetSection("Jwt:Issuer").Value,
                audience: _config.GetSection("Jwt:Audience").Value,
                signingCredentials: signinCred
                );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }


    }
}
