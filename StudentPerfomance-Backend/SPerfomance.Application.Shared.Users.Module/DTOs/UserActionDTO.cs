using System.Text.Json.Serialization;

namespace SPerfomance.Application.Shared.Users.Module.DTOs;

public class UserActionDTO
{
	[JsonPropertyName("name")]
	public string? Name { get; set; }

	[JsonPropertyName("surname")]
	public string? Surname { get; set; }

	[JsonPropertyName("thirdname")]
	public string? Thirdname { get; set; }

	[JsonPropertyName("role")]
	public string? Role { get; set; }

	[JsonPropertyName("email")]
	public string? Email { get; set; }
}
