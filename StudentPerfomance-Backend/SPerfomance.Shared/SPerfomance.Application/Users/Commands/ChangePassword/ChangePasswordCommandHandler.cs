using SPerfomance.Application.Abstractions;
using SPerfomance.Application.Services.Authentication;
using SPerfomance.Application.Services.Authentication.Abstractions;
using SPerfomance.Application.Services.Authentication.Models;
using SPerfomance.Domain.Models.Users;
using SPerfomance.Domain.Models.Users.Abstractions;
using SPerfomance.Domain.Models.Users.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Users.Commands.ChangePassword;

public sealed class ChangePasswordCommandHandler(
    IUsersRepository repository,
    IPasswordHasher hasher
) : ICommandHandler<ChangePasswordCommand, User>
{
    public async Task<Result<User>> Handle(
        ChangePasswordCommand command,
        CancellationToken ct = default
    )
    {
        if (string.IsNullOrWhiteSpace(command.Token))
            return new Error("Необходимо авторизоваться");

        var token = new Token(command.Token);
        if (!await new UserVerificationService(repository).IsVerifiedAny(token, ct))
            return new Error("Необходимо авторизоваться");

        var user = await repository.GetById(token.UserId, ct);
        if (user == null)
            return UserErrors.NotFound();

        if (string.IsNullOrWhiteSpace(command.CurrentPassword))
            return new Error("Пароль авторизации неверный");

        var isVerified = hasher.Verify(command.CurrentPassword, user.HashedPassword);
        if (!isVerified)
            return new Error("Пароль авторизации неверный");

        if (string.IsNullOrWhiteSpace(command.NewPassword))
            return UserErrors.PasswordEmpty();

        if (command.NewPassword.Length < 10)
            return UserErrors.PasswordInvalid();

        var hashedPassword = hasher.Hash(command.NewPassword);
        user.UpdatePassword(hashedPassword);
        await repository.Update(user, ct);
        return user;
    }
}
