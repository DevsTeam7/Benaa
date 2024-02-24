using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Benaa.Core.Entities.General
{
    public class Wallet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public decimal? Amount { get; set; } = 0;
        [ForeignKey(nameof(Student))]
        public string? StudentId { get; set; }
        public virtual User? Student { get; set; }
    }
}
