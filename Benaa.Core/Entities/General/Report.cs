using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Benaa.Core.Entities.General
{
    public class Report
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Problem { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int TargetId { get; set; }
        [Required]
        public string UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
