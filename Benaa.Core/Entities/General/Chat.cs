using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benaa.Core.Entities.General
{
    public class Chat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey(nameof(Sender))]
        public string SenderId { get; set; }
        [ForeignKey(nameof(Receiver))]
        public string ReceiverId { get; set; }

        public ICollection<Messages>? Messages { get; }

        public virtual User? Sender { get; set; }
        public virtual User? Receiver { get; set; }
    }
}
