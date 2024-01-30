using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;

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
        public int StudentId { get; set; }
        [Required]
        public bool IsPurchased { get; set; }

        public virtual Course? Course { get; set; }
        public virtual User? User { get; set; }
    }
}
