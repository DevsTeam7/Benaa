using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benaa.Core.Entities.General
{
    public class Rate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Stars { get; set; }
        [Required]
        [ForeignKey(nameof(User))]
        public int StudentId { get; set; }
        [Required]
        public int CourseId { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public virtual Course? Course { get; set; }
        public virtual User? User { get; set; }
    }
}
