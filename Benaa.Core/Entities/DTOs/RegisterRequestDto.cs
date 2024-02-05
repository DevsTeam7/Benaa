using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benaa.Core.Entities.DTOs
{
    public class RegisterRequestDto
    {
        public string Email { get; set; }
        public string Passwrod { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}
