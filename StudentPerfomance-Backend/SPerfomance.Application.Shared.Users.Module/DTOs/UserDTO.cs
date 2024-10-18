using System.Text.Json.Serialization;

using SPerfomance.Application.Shared.Module.Extensions;
using SPerfomance.Application.Shared.Module.Schemas.Users;
using SPerfomance.Application.Shared.Users.Module.Repositories;

namespace SPerfomance.Application.Shared.Users.Module.DTOs;

public class UserDTO
{
	[JsonPropertyName("name")]
	public string? Name { get; set; }

	[JsonPropertyName("surname")]
	public string? Surname { get; set; }

	[JsonPropertyName("thirdname")]
	public string? Thirdname { get; set; }

	[JsonPropertyName("email")]
	public string? Email { get; set; }

	[JsonPropertyName("password")]
	public string? Password { get; set; }

	[JsonPropertyName("role")]
	public string? Role { get; set; }
}

public static class UserDTOExtensions
{
	public static UserSchema ToSchema(this UserDTO dto)
	{
		if (dto == null)
			return new UserSchema();

		string name = dto.Name.CreateValueOrEmpty();
		string surname = dto.Surname.CreateValueOrEmpty();
		string thirdname = dto.Thirdname.CreateValueOrEmpty();
		string email = dto.Email.CreateValueOrEmpty();

		UserSchema schema = new UserSchema(email, name, surname, thirdname);
		return schema;
	}

	public static UserRepositoryObject ToRepositoryObject(this UserDTO dto)
	{
		UserSchema schema = dto.ToSchema();
		UserRepositoryObject parmaeter = new UserRepositoryObject()
		.WithName(schema.Name)
		.WithSurname(schema.Surname)
		.WithThirdname(schema.Thirdname)
		.WithEmail(schema.Email);

		return parmaeter;
	}
}
