using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Benaa.Core.Entities.General
{
    public class Sceduale
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public decimal Price { get; set; } = 0;

        public ScedualeStatus Status { get; set; } = ScedualeStatus.Still;


        [Required]
        [ForeignKey(nameof(Teacher))]
        public string TeacherId { get; set; }
        [ForeignKey(nameof(Student))]
        public string? StudentId { get; set; } 


        public virtual Chat? Chat { get; set; }
        public virtual User? Teacher { get; set; }
        public virtual User? Student { get; set; }
    }

    public enum ScedualeStatus
    {
        Closed,
        Opened,
        Still,
    }
}
