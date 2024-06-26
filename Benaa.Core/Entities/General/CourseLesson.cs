﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Benaa.Core.Entities.General
{
    public class CourseLesson
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public string? FileUrl { get; set; }
        public Guid? CourseChapterId { get; set; }

        public virtual CourseChapter? CourseChapter { get; set; }
    }
}
