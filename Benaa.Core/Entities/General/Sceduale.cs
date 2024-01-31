using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benaa.Core.Entities.General
{
    public class Sceduale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        [DataType(DataType.Time)]
        public TimeSpan TimeStart { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan TimeEnd { get; set; } 

        [Required]
        [ForeignKey(nameof(Teacher))]
        public string TeacherId { get; set; }
        [ForeignKey(nameof(Student))]
        public string? StudentId { get; set; }
        public virtual User? Teacher { get; set; }
        public virtual User? Student { get; set; }
    }
}
