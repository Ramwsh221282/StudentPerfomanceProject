using SPerfomance.Application.Abstractions;
using SPerfomance.Application.Services.Authentication.Abstractions;
using SPerfomance.Domain.Models.Users;
using SPerfomance.Domain.Models.Users.Abstractions;
using SPerfomance.Domain.Models.Users.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler(
    string generatedPassword,
    IUsersRepository repository,
    IPasswordHasher hasher
) : ICommandHandler<RegisterUserCommand, User>
{
    public async Task<Result<User>> Handle(
        RegisterUserCommand command,
        CancellationToken ct = default
    )
    {
        if (await repository.HasWithEmail(command.Email, ct))
            return Result<User>.Failure(UserErrors.EmailDublicate(command.Email));

        var hashedPassword = hasher.Hash(generatedPassword);

        var user = User.Create(
            command.Name,
            command.Surname,
            command.Patronymic,
            command.Email,
            command.Role,
            hashedPassword
        );

        if (user.IsFailure)
            return user;

        await repository.Insert(user.Value, ct);
        return user;
    }
}
