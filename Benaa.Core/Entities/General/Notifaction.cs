using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benaa.Core.Entities.General
{
    public class Notifaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public bool? Read { get; set; } = false;
        [Required]
        public int UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
