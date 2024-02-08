using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benaa.Core.Entities.DTOs
{
    public class StudentRegisterDto
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        public string? ImageUrl { get; set; }
        [Required]
        public string? EducationLevel { get; set; } = string.Empty;
        public string? Specialization { get; set; } = string.Empty;
        public string? Experience { get; set; } = string.Empty;
        public string? University { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? Contry { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public bool? IsAgreedToTerms { get; set; }
        public bool? Gender { get; set; }


        //F.K
        public Guid? WalletId { get; set; }
        public Guid? CertificationId { get; set; }
        public Guid? BankInformationId { get; set; }

    }
}
