using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.Users;

namespace SPerfomance.Application.Users.Commands.ChangeEmail;

public sealed class ChangeEmailCommand : ICommand<User>
{
    public string? Token { get; set; }
    public string? CurrentEmail { get; set; }
    public string? NewEmail { get; set; }
    public string? Password { get; set; }
}
