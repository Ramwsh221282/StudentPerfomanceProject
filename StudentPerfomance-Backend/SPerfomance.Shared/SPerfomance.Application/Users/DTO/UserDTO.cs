using SPerfomance.Domain.Models.Users;

namespace SPerfomance.Application.Users.DTO;

public class UserDTO
{
    public int Number { get; init; }

    public string Name { get; init; }

    public string Surname { get; init; }

    public string? Patronymic { get; init; }

    public string Email { get; init; }

    public string Role { get; init; }

    public DateTime LastTimeOnline { get; init; }

    public DateTime RegisteredDate { get; init; }

    public UserDTO(User user)
    {
        Number = user.EntityNumber;
        Name = user.Name.Name;
        Surname = user.Name.Surname;
        Patronymic = user.Name.Patronymic;
        Email = user.Email.Email;
        Role = user.Role.Role;
        LastTimeOnline = user.LastLoginDate;
        RegisteredDate = user.RegisteredDate;
    }
}

public static class UserDTOExtensions
{
    public static UserDTO MapFromDomain(this User user) => new UserDTO(user);
}
