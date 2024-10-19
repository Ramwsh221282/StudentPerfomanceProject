using SPerfomance.Application.Shared.Module.Extensions;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.Shared.Users.Module.API.Responses;

public class UserResponse
{
	public string Name { get; init; }
	public string Surname { get; init; }
	public string Thirdname { get; init; }
	public string Role { get; init; }
	public string Email { get; init; }
	public DateTime LastTimeOnline { get; init; }
	public DateTime RegisteredDate { get; init; }
	public int Number { get; init; }

	public UserResponse(User user)
	{
		Name = user.Name.Name;
		Surname = user.Name.Surname;
		Thirdname = user.Name.Thirdname.CreateValueOrEmpty();
		Role = user.Role.Value;
		LastTimeOnline = user.LastLoginDate;
		RegisteredDate = user.RegisteredDate;
		Email = user.Email.Email;
		Number = user.EntityNumber;
	}
}
