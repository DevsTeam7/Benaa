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
        [DataType(DataType.Time)]
        public TimeSpan TimeStart { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan TimeEnd { get; set; }
        [Required]
        public string TeacherId { get; set; }
        public string? StudentId { get; set; }
    }
}
