using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.Users;

namespace SPerfomance.Application.Users.Commands.ChangeEmail;

public sealed class ChangeEmailCommand : ICommand<User>
{
    public string? Token { get; }
    public string? CurrentEmail { get; }
    public string? NewEmail { get; }
    public string? Password { get; }
}
