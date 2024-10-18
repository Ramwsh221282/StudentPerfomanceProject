using System.Text.Json.Serialization;

using SPerfomance.Application.Shared.Module.Extensions;
using SPerfomance.Application.Shared.Module.Schemas.Users;

namespace SPerfomance.Application.Shared.Users.Module.DTOs;

public class UserRegistrationDTO
{
	[JsonPropertyName("email")]
	public string? Email { get; set; }

	[JsonPropertyName("name")]
	public string? Name { get; set; }

	[JsonPropertyName("surname")]
	public string? Surname { get; set; }

	[JsonPropertyName("thirdname")]
	public string? Thirdname { get; set; }

	[JsonPropertyName("role")]
	public string? Role { get; set; }
}

public static class UserRegistrationDTOExtensions
{
	public static UserSchema ToSchema(this UserRegistrationDTO dto)
	{
		if (dto == null)
			return new UserSchema();

		string name = dto.Name.CreateValueOrEmpty();
		string surname = dto.Surname.CreateValueOrEmpty();
		string thirdname = dto.Thirdname.CreateValueOrEmpty();
		string email = dto.Email.CreateValueOrEmpty();
		string role = dto.Role.CreateValueOrEmpty();

		UserSchema schema = new UserSchema(email, name, surname, thirdname);
		schema.Role = role;
		return schema;
	}
}
