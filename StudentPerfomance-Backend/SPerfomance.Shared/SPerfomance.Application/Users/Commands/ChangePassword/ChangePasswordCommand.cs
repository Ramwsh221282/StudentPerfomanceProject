using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.Users;

namespace SPerfomance.Application.Users.Commands.ChangePassword;

public sealed class ChangePasswordCommand : ICommand<User>
{
    public string? Token { get; set; }
    public string? CurrentPassword { get; set; }
    public string? NewPassword { get; set; }
}
