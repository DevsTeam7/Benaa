﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Benaa.Core.Entities.General
{
    public class Messages
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string Type { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public int ChatId { get; set; }
        public virtual Chat? Chat { get; set; }
    }
}
