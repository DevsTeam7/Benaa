using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benaa.Core.Entities.General
{
    public class CourseChapter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public  int CourseId { get; set; }
        public virtual Course? Course { get; set;}

        public ICollection<CourseLesson>? CourseLessons { get; }
    }
}
