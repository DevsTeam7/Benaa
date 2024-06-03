using Microsoft.AspNetCore.Identity;

namespace Benaa.Core.Entities.DTOs
{
    public class LoginRequestDto
    {
       public required string Email { get; set; }
       public required string Password { get; set; }
     
    }
}
