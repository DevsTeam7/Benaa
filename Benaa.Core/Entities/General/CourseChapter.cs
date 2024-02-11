using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Benaa.Core.Entities.General
{
    public class CourseChapter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public Guid CourseId { get; set; }
        public virtual Course? Course { get; set; }

        public ICollection<CourseLesson>? CourseLessons { get; }
    }
}
