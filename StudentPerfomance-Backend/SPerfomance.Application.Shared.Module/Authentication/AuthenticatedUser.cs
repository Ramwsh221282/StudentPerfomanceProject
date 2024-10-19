using SPerfomance.Application.Shared.Module.Extensions;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.Shared.Module.Authentication;

public class AuthenticatedUser
{
	public string Name { get; init; }
	public string Surname { get; init; }
	public string Thirdname { get; init; }
	public string Email { get; init; }
	public string Token { get; init; }
	public string Role { get; init; }

	public AuthenticatedUser(string token, User user)
	{
		Name = user.Name.Name;
		Surname = user.Name.Surname;
		Thirdname = user.Name.Thirdname.CreateValueOrEmpty();
		Email = user.Email.Email;
		Token = token;
		Role = user.Role.Value;
	}
}
