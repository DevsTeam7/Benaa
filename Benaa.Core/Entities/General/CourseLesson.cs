using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Benaa.Core.Entities.General
{
    public class CourseLesson
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public int CourseChapterId { get; set; }
        public virtual CourseChapter? CourseChapter { get; set; }
    }
}
