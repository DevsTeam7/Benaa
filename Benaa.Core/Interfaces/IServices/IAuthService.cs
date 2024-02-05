using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;

namespace Benaa.Core.Interfaces.IServices
{
    public interface IAuthService
    {
        string GenerateTokenString(User user, IList<string> roles);
        Task<User> Registration(RegisterRequestDto user);
        Task<string> Login(LoginRequestDto ApplicationUser);
    }
}
