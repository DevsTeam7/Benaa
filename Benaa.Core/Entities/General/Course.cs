﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benaa.Core.Entities.General
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, StringLength(maximumLength: 100, MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string VideoUrl { get; set; }
        [Required]  
        public string Type { get; set; }
        [Required]
        public string ImageUrl { get; set; }

        public string? TargtedPeople { get; set; }
        public string? GoalsDescription { get; set; }
        public string? ContactUrl { get; set; }
        public bool? IsFiles { get; set; }
        public bool? IsRecorded { get; set; }
        public bool IsPublished { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        [Required]
        [ForeignKey(nameof(User))]
        public int TeacherId { get; set; }

        public ICollection<UserCourses>? UserCourses { get; set; }
        public ICollection<CourseChapter>? CourseChapters { get; set; }
        public ICollection<Rate>? Rates { get; set; }

        public virtual User? User { get; set; }


    }
}
