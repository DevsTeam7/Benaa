namespace Benaa.Core.Entities.DTOs
{
    public class StudentRegisterDto
    {
        public required string FirstName { get; set; }
        public string? City { get; set; }
        public string? Contry { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? Gender { get; set; }
        public int? PhoneNumber { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

    }
}
