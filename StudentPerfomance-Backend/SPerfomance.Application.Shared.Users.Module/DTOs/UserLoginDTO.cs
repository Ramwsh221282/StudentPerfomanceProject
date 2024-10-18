using System.Text.Json.Serialization;

namespace SPerfomance.Application.Shared.Users.Module.DTOs;

public class UserLoginDTO
{
	[JsonPropertyName("email")]
	public string? Email { get; set; }

	[JsonPropertyName("password")]
	public string? Password { get; set; }
}
