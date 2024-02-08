using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benaa.Core.Entities.DTOs
{
    public class TeacherRegisterDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ImageUrl { get; set; }
        public string? EducationLevel { get; set; } = string.Empty;
        public string? Specialization { get; set; } = string.Empty;
        public string? Experience { get; set; } = string.Empty;
        public string? University { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? Contry { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public bool? IsAgreedToTerms { get; set; }
        public bool? IsApproved { get; set; } = false;
        public bool? Gender { get; set; }


        //F.K
        public Guid? WalletId { get; set; }
        public Guid? CertificationId { get; set; }
        public Guid? BankInformationId { get; set; }

    }
}
