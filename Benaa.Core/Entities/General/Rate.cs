using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Benaa.Core.Entities.General
{
    public class Rate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public float Stars { get; set; }
        [Required]
        [ForeignKey(nameof(User))]
        public string StudentId { get; set; }
        [Required]
        public int CourseId { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public virtual Course? Course { get; set; }
        public virtual User? Student { get; set; }
    }
}
