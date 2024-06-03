namespace Benaa.Core.Entities.DTOs
{
	public class LoginResponseDto
	{
		public required string Token { get; set; }
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public required string ImageUrl { get; set; }
		public required bool EmailConfirmed { get; set; }
		public required string Role { get; set; }
	}

}
