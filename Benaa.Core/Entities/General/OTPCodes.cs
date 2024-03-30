using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Benaa.Core.Entities.General
{
    public class OTPCodes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Code { get; set; }
        public bool Used { get; set; }
        public OtpType Type { get; set; }

        public required string UserId { get; set; }
        public virtual User? User { get; set; }
    }

    public enum OtpType
    {
        Confirmation,
        ResetPassword
    }

}

