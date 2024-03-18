using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benaa.Core.Entities.DTOs
{
    public class ReportDto
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public string Problem { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public Guid TargetId { get; set; }
        [Required]
        public string UserId { get; set; }

    }
}
