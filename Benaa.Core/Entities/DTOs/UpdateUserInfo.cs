﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Benaa.Core.Entities.DTOs
{
    public class UpdateUserInfo
    {
        public string? Id { get; set; }
        //public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public bool? IsApproved { get; set; } = false;
        //public bool? Gender { get; set; }
        //public string? Role { get; set; }

       
        //public Guid? BankInformationId { get; set; }
    }
}
