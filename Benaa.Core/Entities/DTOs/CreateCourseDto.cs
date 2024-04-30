using Microsoft.AspNetCore.Http;
using static Benaa.Core.Entities.General.Course;

namespace Benaa.Core.Entities.DTOs
{
    public class CreateCourseDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required decimal Price { get; set; }
        public required IFormFile VideoUrl { get; set; }
        public required int Type { get; set; }
        public required IFormFile ImageUrl { get; set; }

        public string? TargtedPeople { get; set; }
        public string? GoalsDescription { get; set; }
        public bool? IsFiles { get; set; }
        public bool? IsRecorded { get; set; }
        public bool IsPublished { get; set; }
    }

}
