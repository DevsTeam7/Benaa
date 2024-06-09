using Microsoft.AspNetCore.Http;
namespace Benaa.Core.Entities.DTOs
{
    public class TeacherRegisterDto : StudentRegisterDto
    {
		public required string LastName { get; set; }
		public string? EducationLevel { get; set; } = string.Empty;
        public required string Specialization { get; set; }
        public required string Experience { get; set; }
        public required string University { get; set; }
        public string? Description { get; set; }
        public bool? IsAgreedToTerms { get; set; } 
    }
}
