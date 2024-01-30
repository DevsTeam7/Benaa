
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Benaa.Core.Entities.General
{
    public class User : IdentityUser 
    {

        [Required, StringLength(maximumLength: 100, MinimumLength = 2)]
        public string FirstName { get; set; } 
        [Required, StringLength(maximumLength: 100, MinimumLength = 2)]
        public string LastName { get; set; }

        public string? EducationLevel { get; set; } = string.Empty;
        public string? Specialization { get; set; } = string.Empty;
        public string? Experience { get; set; } = string.Empty;
        public string? University { get; set; } = string.Empty;
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? Contry { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public bool IsAgreedToTerms { get; set; }
        public bool? IsApproved { get; set; } = false;
        public bool? Gender { get; set; }


        //F.K
        public int? WalletId { get; set; }
        public int? CertificationId { get; set; }
        public int? BankInformationId { get; set; }


        public ICollection<Payment>? TeacherDues { get; set; } 
        public ICollection<Payment>? StudentPayments { get; set; }
        public ICollection<Notifaction>? Notifactions { get; set; }
        public ICollection<Report>? Reports { get; set; }
        public ICollection<Chat>? SenderChats { get; set; }
        public ICollection<Chat>? ReceiverChats { get; set; }
        public ICollection<Course>? Courses { get; set; }
        public ICollection<UserCourses>? UserCourses { get; set; }
        public ICollection<Rate>? Rates { get; set; }
        public ICollection<Sceduale>? Appointments { get; set; }
        public ICollection<Sceduale>? Sceduales { get; set; }

        public virtual Wallet? Wallet { get; set; }
        public virtual Certification? Certification { get; set; }
        public virtual BankInformation? BankInformation { get; set; }

        //Role??
    }
}
