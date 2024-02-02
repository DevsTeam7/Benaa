using Benaa.Core.Entities.General;
using Microsoft.AspNetCore.Identity;
using Benaa.Core.Interfaces.IServices;
using Benaa.Core.Entities.DTOs;

namespace Benaa.Core.Services
{
    public class AuthService : IAuthService
    {
        public readonly UserManager<User> _userManager;
        public AuthService(UserManager<User> userManager)
        {
            _userManager = userManager;
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
    }
}
