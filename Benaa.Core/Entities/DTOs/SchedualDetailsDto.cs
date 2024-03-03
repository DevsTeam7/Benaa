using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Benaa.Core.Entities.DTOs;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benaa.Core.Entities.DTOs
{
    public class SchedualDetailsDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        public int TimeStart { get; set; }
         public int TimeEnd { get; set; }
        public decimal Price { get; set; } = 0;

        [Required]
         public string TeacherId { get; set; }
        public string ?StudentId { get; set; } 
    }
}
