using Microsoft.AspNetCore.Http;

namespace Benaa.Core.Entities.DTOs
{
    public class CreateLessonDto
    {
        public required string Name { get; set; }
        public required string CourseChapterId { get; set; }
        public required IFormFile FileUrl { get; set; }
    }
}
