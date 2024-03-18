using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benaa.Core.Entities.DTOs
{
    public class SchedualDto
    {
        public DateTime Date { get; set; } = DateTime.Today;
        
        public DateTime TimeStart { get; set; }
     
        public DateTime TimeEnd { get; set; }
        public decimal Price { get; set; } = 0;

        [Required]
        public string TeacherId { get; set; }
        public string? StudentId { get; set; }
    }
}
