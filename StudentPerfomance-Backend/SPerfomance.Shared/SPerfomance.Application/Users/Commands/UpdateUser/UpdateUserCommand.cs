using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.Users;
using SPerfomance.Domain.Models.Users.Abstractions;
using SPerfomance.Domain.Models.Users.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Users.Commands.UpdateUser;

public sealed record UpdateUserCommand(Guid Id, string Name, string Surname, string Patronymic)
    : ICommand<User>;

public sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, User>
{
    private readonly IUsersRepository _repository;

    public UpdateUserCommandHandler(IUsersRepository repository) => _repository = repository;

    public async Task<Result<User>> Handle(
        UpdateUserCommand command,
        CancellationToken ct = default
    )
    {
        User? user = await _repository
            .GetById(command.Id.ToString().ToUpper(), ct)
            .ConfigureAwait(false);
        if (user == null)
            return UserErrors.NotFound();
        Result<User> updated = user.ChangeName(command.Name, command.Surname, command.Patronymic);
        if (updated.IsFailure)
            return updated.Error;
        await _repository.Update(updated, ct);
        return user;
    }
}
