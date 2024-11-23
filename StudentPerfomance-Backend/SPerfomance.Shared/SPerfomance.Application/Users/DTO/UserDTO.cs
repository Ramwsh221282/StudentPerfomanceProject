using SPerfomance.Domain.Models.Users;

namespace SPerfomance.Application.Users.DTO;

public class UserDto(User user)
{
    public int Number { get; init; } = user.EntityNumber;
    public string Name { get; init; } = user.Name.Name;
    public string Surname { get; init; } = user.Name.Surname;
    public string? Patronymic { get; init; } = user.Name.Patronymic;
    public string Email { get; init; } = user.Email.Email;
    public string Role { get; init; } = user.Role.Role;
    public DateTime LastTimeOnline { get; init; } = user.LastLoginDate;
    public DateTime RegisteredDate { get; init; } = user.RegisteredDate;
}

public static class UserDtoExtensions
{
    public static UserDto MapFromDomain(this User user) => new(user);
}
