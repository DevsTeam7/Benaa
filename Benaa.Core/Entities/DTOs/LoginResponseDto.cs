namespace Benaa.Core.Entities.DTOs
{
	public class LoginResponseDto
	{
		public string? Token { get; set; }
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public required string ImageUrl { get; set; }
		public bool? EmailConfirmed { get; set; }
		public string? Role { get; set; }
		public string? Description { get; set; }
	}

}
