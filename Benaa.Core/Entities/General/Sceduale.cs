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
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; } 

        [Required]
        public string TeacherId { get; set; }
        public string? StudentId { get; set; }
        public virtual User? Teacher { get; set; }
        public virtual User? Student { get; set; }
    }
}
