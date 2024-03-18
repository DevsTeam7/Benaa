﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Benaa.Core.Entities.General
{
    public class Sceduale
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        //[DataType(DataType.Time)]
        public int TimeStart { get; set; }
        public int TimeEnd { get; set; }
        public decimal Price { get; set; } = 0;
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public int TimeStart { get; set; }
        public int TimeEnd { get; set; }
        public ScedualeStatus Status { get; set; } = ScedualeStatus.Still;


        [Required]
        [ForeignKey(nameof(Teacher))]
        public string TeacherId { get; set; }
        [ForeignKey(nameof(Student))]

        public string? StudentId { get; set; } 
        public virtual User Teacher { get; set; }
        public virtual User? Student { get; set; } 
        public string? StudentId { get; set; }

        public virtual Chat? Chat { get; set; }
        public virtual User? Teacher { get; set; }
        public virtual User? Student { get; set; }
    }

    public enum ScedualeStatus
    {
        Closed,
        Opened,
        Still,
    }
}
