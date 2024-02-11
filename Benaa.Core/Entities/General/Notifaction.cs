using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Benaa.Core.Entities.General
{
    public class Notifaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public bool? Read { get; set; } = false;
        [Required]
        public string UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
