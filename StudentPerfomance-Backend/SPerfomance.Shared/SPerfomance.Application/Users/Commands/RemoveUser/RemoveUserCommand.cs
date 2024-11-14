using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.Users;

namespace SPerfomance.Application.Users.Commands.RemoveUser;

public class RemoveUserCommand(User? user) : ICommand<User>
{
    public User? User { get; init; } = user;
}
