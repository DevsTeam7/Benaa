using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Benaa.Core.Entities.General
{
    public class MoneyCode
    {
       [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string Code { get; set; }
        public bool? Status { get; set; } = false;
        
        public int Amount { get; set; }
    }
}
