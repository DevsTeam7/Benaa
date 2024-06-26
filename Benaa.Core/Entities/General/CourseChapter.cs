﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Benaa.Core.Entities.General
{
    public class CourseChapter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid? CourseId { get; set; }

        public virtual Course? Course { get; set; }
        public List<CourseLesson>? CourseLessons { get; }
    }
}
