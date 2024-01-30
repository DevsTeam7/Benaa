
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
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, StringLength(maximumLength: 100, MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;
        [Required, StringLength(maximumLength: 100, MinimumLength = 2)]
        public string LastName { get; set; } = string.Empty;
        [Required, DataType(DataType.EmailAddress), StringLength(maximumLength: 100, MinimumLength = 5)]
        public string Email { get; set; } = string.Empty;
        [Required, DataType(DataType.Password), StringLength(maximumLength: 100, MinimumLength = 5)]
        public string Password { get; set; } = string.Empty;
        public string EducationLevel { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string Experience { get; set; } = string.Empty;
        public string University { get; set; } = string.Empty;
        [Required, DataType(DataType.PhoneNumber)]
        public string Phone { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Contry { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public bool IsAgreedToTerms { get; set; }
        public bool IsApproved { get; set; }
        public bool Gender { get; set; }
        public int WalletId { get; set; }


        public ICollection<Payment> TeacherDues { get; set; } 
        public ICollection<Payment> StudentPayments { get; set; }
        
        public virtual Wallet? Wallet { get; set; }
        //Role,Certification,Bank id's
    }
}
