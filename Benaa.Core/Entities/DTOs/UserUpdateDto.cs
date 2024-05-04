using Microsoft.AspNetCore.Http;

namespace Benaa.Core.Entities.DTOs
{
    public class UserUpdateDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EducationLevel { get; set; }
        public string? Specialization { get; set; }
        public string? Experience { get; set; }
        public string? University { get; set; }
        public string? City { get; set; }
        public string? Contry { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? Gender { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
