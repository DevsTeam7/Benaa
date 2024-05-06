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
       // public NotificationType? type { get; set; }
        [NotMapped]
        public SchedualeNotificationDto? notificationObject { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public bool? IsRead { get; set; } = false;
        [Required]
        public string UserId { get; set; }
        public virtual User? User { get; set; }
    }
    //public enum NotificationType
    //{
    //    BookedAppointment = 0,
    //    Sceduale = 1,
    //    Rate = 2,
    //    CancleAppointment = 3,
    //    NewMessage = 4,
    //    Approve = 5
    //}
}
