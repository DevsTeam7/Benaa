using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using ErrorOr;

namespace Benaa.Core.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<ErrorOr<LoginRequestDto.Response>> Login(LoginRequestDto.Request applictionUser);
        Task<ErrorOr<User?>> Registration(RegisterRequestDto newUser);
    }
}
