using SPerfomance.Application.Users.Queries.GetUserByEmail;

namespace SPerfomance.Api.Features.Users.Contracts;

public class UserContract
{
	public string? Name { get; set; }

	public string? Surname { get; set; }

	public string? Patronymic { get; set; }

	public string? Email { get; set; }

	public string? Role { get; set; }

	public static implicit operator GetUserByEmailQuery(UserContract contract) =>
		new GetUserByEmailQuery(contract.Email);
}
