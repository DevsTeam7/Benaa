using Benaa.Core.Entities.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Benaa.Core.Entities.General
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string Content { get; set; }
        [NotMapped]
        public SchedualeNotificationDto? notificationObject { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public bool? IsRead { get; set; } = false;
        [Required]
        public string UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
