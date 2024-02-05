using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benaa.Core.Interfaces.IServices
{
    public interface IAuthService
    {
        string GenerateTokenString(User user, IList<string> roles);
        Task<User> Registration(RegisterRequestDto user);
        Task<string> Login(LoginRequestDto ApplicationUser);
    }
}
