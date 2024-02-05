using Benaa.Core.Entities.General;
using Microsoft.AspNetCore.Identity;
using Benaa.Core.Interfaces.IServices;
using Benaa.Core.Entities.DTOs;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Benaa.Core.Services
{
    public class AuthService : IAuthService
    {
        public readonly UserManager<User> _userManager;
        public readonly RoleManager<IdentityRole> _roleManager;
        public readonly IConfiguration _config;
    

        public AuthService(UserManager<User> userManager, IConfiguration config, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _config = config;
        }

        //public async Task<IEnumerable<IdentityResult>> RegisterUser(RegisterRequestDto _user)
        //{

        //    User user = new User();

        //    user.Email = _user.Email;
        //    user.UserName = _user.UserName;
        //    var result = await _userManager.CreateAsync(user, _user.Passwrod);
        //    return result;
        //}

        public async Task<string> Registeration(RegisterRequestDto newUser)
        {
            var userExists = await _userManager.FindByNameAsync(newUser.UserName);
            if (userExists != null)
                return ("User already exists");

            User user = new()
            {
                Email = newUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = newUser.UserName,
            };
            var createUserResult = await _userManager.CreateAsync(user, newUser.Passwrod);
            if (!createUserResult.Succeeded)
                return ("User creation failed! Please check user details and try again.");

            if (!await _roleManager.RoleExistsAsync(newUser.Role))
                await _roleManager.CreateAsync(new IdentityRole(newUser.Role));

            if (await _roleManager.RoleExistsAsync(newUser.Role))
                await _userManager.AddToRoleAsync(user, newUser.Role);

            return ("User created successfully!");
        }

        public async Task<IActionResult> Login(RegisterRequestDto ApplictionUser)
        {
            var user = await _userManager.FindByEmailAsync(ApplictionUser.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, ApplictionUser.Passwrod))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var token = GenerateTokenString(user, userRoles);

              //  return Ok(token);
            }
            //return token;
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
            SigningCredentials signinCred = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
            JwtSecurityToken securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                issuer: _config.GetSection("Jwt:Audience").Value,
                audience: _config.GetSection("Jwt:Issuer").Value,
                signingCredentials: signinCred
                );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }


    }
}
