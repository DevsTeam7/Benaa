using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Benaa.Core.Entities.General
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public decimal Amount { get; set; } = 0;
        [Required]
        public string Type { get; set; }
        [Required]
        public Guid ItemId { get; set; }
        public int Status { get; set; } = 0;
        public DateTimeOffset CreatedAt { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

        [Required]
        [ForeignKey(nameof(Teacher))]
        public string TeacherId { get; set; }
        [Required]
        [ForeignKey(nameof(Student))]
        public string StudentId { get; set; }
        public virtual User? Teacher { get; set; }
        public virtual User? Student { get; set; }
    }
}
