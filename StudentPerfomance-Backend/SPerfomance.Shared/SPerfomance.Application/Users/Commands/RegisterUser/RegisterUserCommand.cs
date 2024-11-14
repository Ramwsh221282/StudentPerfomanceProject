using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.Users;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Users.Commands.RegisterUser;

public class RegisterUserCommand(
    string? name,
    string? surname,
    string? patronymic,
    string? email,
    string? role
) : ICommand<User>
{
    public string Name { get; init; } = name.ValueOrEmpty();

    public string Surname { get; init; } = surname.ValueOrEmpty();

    public string Patronymic { get; init; } = patronymic.ValueOrEmpty();

    public string Email { get; init; } = email.ValueOrEmpty();

    public string Role { get; init; } = role.ValueOrEmpty();
}
