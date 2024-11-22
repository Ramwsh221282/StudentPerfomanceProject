using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.Users;
using SPerfomance.Domain.Models.Users.Abstractions;
using SPerfomance.Domain.Models.Users.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Users.Commands.RemoveUser;

public class RemoveUserCommandHandler(IUsersRepository repository)
    : ICommandHandler<RemoveUserCommand, User>
{
    public async Task<Result<User>> Handle(
        RemoveUserCommand command,
        CancellationToken ct = default
    )
    {
        if (command.User == null)
            return Result<User>.Failure(UserErrors.NotFound());

        await repository.Remove(command.User, ct);
        return Result<User>.Success(command.User);
    }
}
