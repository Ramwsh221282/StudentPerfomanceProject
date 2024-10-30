using SPerfomance.Domain.Models.Users;

namespace SPerfomance.Application.Users.DTO;

public class UserDTO
{
	public string? Name { get; set; }

	public string? Surname { get; set; }

	public string? Patronymic { get; set; }

	public string? Email { get; set; }

	public string? Role { get; set; }
}

public static class UserDTOExtensions
{
	public static UserDTO MapFromDomain(this User user) =>
		new UserDTO()
		{
			Name = user.Name.Name,
			Surname = user.Name.Surname,
			Patronymic = user.Name.Patronymic,
			Email = user.Email.Email,
			Role = user.Role.Role
		};
}
