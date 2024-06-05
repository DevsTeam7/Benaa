using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Benaa.Core.Entities.General
{
    public class Messages
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public MessagesType Type { get; set; }
        public DateTimeOffset? SendAt { get; set; } = DateTimeOffset.UtcNow;
        public bool IsRead { get; set; } = false;

        public string UserId { get; set; }
        public virtual User? User { get; set; }

        [Required]
        public Guid ChatId { get; set; }
        public virtual Chat? Chat { get; set; }

    }


    public enum MessagesType
    {
        Text,
        Voice,
        Picture,
    }
}


