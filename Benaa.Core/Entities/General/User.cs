using Microsoft.AspNetCore.Identity;

namespace Benaa.Core.Entities.General
{
    public class User : IdentityUser
    {

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ImageUrl { get; set; }
        public string? CertificationUrl { get; set; }
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
       // public string? Role { get; set; }


        //F.K
        public Guid? WalletId { get; set; }
        public Guid? BankInformationId { get; set; }


        public ICollection<Notification>? Notifactions { get; }
        public ICollection<Report>? Reports { get; }
        public ICollection<Course>? Courses { get; }
        public ICollection<UserCourses>? UserCourses { get; }
        public ICollection<Messages>? Messages { get; }
        public List<Sceduale>? Sceduales { get; set; }

        public virtual Wallet? Wallet { get; set; }
        public virtual BankInformation? BankInformation { get; set; }
    }
}
