using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Benaa.Core.Entities.General
{
    public class Sceduale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        public int TimeStart { get; set; }
        public int TimeEnd { get; set; }
        public ScedualeStatus Status { get; set; } = ScedualeStatus.Still;


        [Required]
        [ForeignKey(nameof(Teacher))]
        public string TeacherId { get; set; }
        [ForeignKey(nameof(Student))]
        public string? StudentId { get; set; }
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
