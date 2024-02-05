using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Benaa.Core.Entities.General
{
    public class UserCourses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        [ForeignKey(nameof(User))]
        public string StudentId { get; set; }
        [Required]
        public bool IsPurchased { get; set; }

        public virtual Course? Course { get; set; }
        public virtual User? Student { get; set; }
    }
}
