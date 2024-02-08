using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Benaa.Core.Entities.General
{
    public class BankInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required, StringLength(maximumLength: 100, MinimumLength = 5)]
        public string FullName { get; set; }
        [Required]
        public string BankName { get; set; }
        [Required]
        public long Account_Number { get; set; }


        public virtual User? Teacher { get; set; }


    }
}
