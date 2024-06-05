using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Benaa.Core.Entities.General
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? VideoUrl { get; set; }
        public CourseType Type { get; set; }
        public string? ImageUrl { get; set; }
        public string? TeacherName;

        public string? TargtedPeople { get; set; }
        public string? GoalsDescription { get; set; }
        public bool? IsFiles { get; set; }
        public bool? IsRecorded { get; set; }
        public bool? IsPublished { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        [ForeignKey(nameof(User))]
        public string? TeacherId { get; set; }

        public List<CourseChapter>? CourseChapters { get; set; }
        public List<Rate>? Rates { get; set; }

        public virtual User? User { get; set; }



        public enum CourseType
        {
            HighShcool = 0,
            College = 1,
            General = 2
        }


    }
}
