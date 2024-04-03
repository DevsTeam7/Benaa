using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Benaa.Core.Entities.General
{
    public class OTPCodes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public required string Code { get; set; }
        public OtpType Type { get; set; }

        public required string UserId { get; set; }
        public virtual User? User { get; set; }
    }

    public enum OtpType
    {
        Confirmation = 0,
        ResetPassword = 1,
    }

}

