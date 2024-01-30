using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benaa.Core.Entities.General
{
    public class BankInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, StringLength(maximumLength: 100, MinimumLength = 5)]
        public string FullName { get; set; } 
        [Required]
        public string BankName { get; set; }
        [Required]
        public long Account_Number { get; set;}


        public virtual User? Teacher { get; set; }


    }
}
