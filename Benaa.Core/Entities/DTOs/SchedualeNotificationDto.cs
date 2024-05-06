using Microsoft.EntityFrameworkCore;

namespace Benaa.Core.Entities.DTOs
{
    [Keyless]
    public class SchedualeNotificationDto
    {
        public string? FullName {  get; set; }
        public DateTime? Date {  get; set; }
        public string? Time {  get; set; }
        public string? chatId { get; set; }
    }
}
