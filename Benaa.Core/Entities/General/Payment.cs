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
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Decimal Amount { get; set; } = new Decimal(0);
        public string Type { get; set; } = string.Empty;
        public int ItemId { get; set; }
        public bool Status { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;


        public int? TeacherId { get; set; }
        public int? StudentId { get; set; }
        public virtual  User? Teacher { get; set; }
        public virtual User? Student { get; set; } 
    }
}
