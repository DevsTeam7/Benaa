namespace Benaa.Core.Entities.DTOs
{
    public class LoginRequestDto
    {
        public class Request
        {
            public required string Email { get; set; }
            public required string Password { get; set; }
        }

        public class Response
        {
            public required string Token { get; set; }
            public required string FirstName { get; set; }
            public required string LastName { get; set; }
            public required bool ImageUrl { get; set; }
        }
    }
}
