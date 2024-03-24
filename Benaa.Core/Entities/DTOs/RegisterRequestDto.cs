using Microsoft.AspNetCore.Http;

namespace Benaa.Core.Entities.DTOs
{
    public class RegisterRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public class StudentRegisterDto
        {
            public required string FirstName { get; set; }
            public required string LastName { get; set; }
            public required IFormFile ImageUrl { get; set; }
            public string? City { get; set; }
            public string? Contry { get; set; }
            public DateTime? DateOfBirth { get; set; }
            public bool? Gender { get; set; }
            public int? PhoneNumber { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
        public class TeacherRegisterDto
        {
            public required string FirstName { get; set; }
            public required string LastName { get; set; }
            public required IFormFile ImageUrl { get; set; }
            public required IFormFile Certification { get; set; }
            public string? EducationLevel { get; set; } = string.Empty;
            public required string Specialization { get; set; }
            public required string Experience { get; set; }
            public required string University { get; set; }
            public string? City { get; set; }
            public string? Contry { get; set; } 
            public DateTime? DateOfBirth { get; set; }
            public required string Description {  get; set; }
            public bool? IsAgreedToTerms { get; set; }
            public bool? Gender { get; set; }
            public int? PhoneNumber { get; set; }
            public required string Email { get; set; }
            public required string Password { get; set; }
        }
    }
}
