﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Benaa.Core.Entities.General
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public decimal? Amount { get; set; } = 0;
        [Required]
        public string Type { get; set; }
        [Required]
        public int ItemId { get; set; }
        public bool? Status { get; set; } = false;
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        [Required]
        [ForeignKey(nameof(Teacher))]
        public string TeacherId { get; set; }
        [Required]
        [ForeignKey(nameof(Student))]
        public string StudentId { get; set; }
        public virtual User? Teacher { get; set; }
        public virtual User? Student { get; set; }
    }
}
