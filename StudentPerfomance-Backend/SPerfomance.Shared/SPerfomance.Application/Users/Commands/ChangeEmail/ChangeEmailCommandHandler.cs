using SPerfomance.Application.Abstractions;
using SPerfomance.Application.Services.Authentication;
using SPerfomance.Application.Services.Authentication.Abstractions;
using SPerfomance.Application.Services.Authentication.Models;
using SPerfomance.Domain.Models.Users;
using SPerfomance.Domain.Models.Users.Abstractions;
using SPerfomance.Domain.Models.Users.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Users.Commands.ChangeEmail;

public sealed class ChangeEmailCommandHandler(IPasswordHasher hasher, IUsersRepository repository)
    : ICommandHandler<ChangeEmailCommand, User>
{
    public async Task<Result<User>> Handle(
        ChangeEmailCommand command,
        CancellationToken ct = default
    )
    {
        if (string.IsNullOrWhiteSpace(command.Token))
            return new Error("Необходимо авторизоваться");

        var token = new Token(command.Token);
        if (!await new UserVerificationService(repository).IsVerifiedAny(token, ct))
            return new Error("Необходимо авторизоваться");

        if (string.IsNullOrWhiteSpace(command.NewEmail))
            return UserErrors.EmailEmpty();

        if (string.IsNullOrWhiteSpace(command.Password))
            return UserErrors.PasswordInvalid();

        var user = await repository.GetByEmail(command.CurrentEmail, ct);
        if (user == null)
            return UserErrors.NotFound();

        var isVerified = hasher.Verify(command.Password, user.HashedPassword);
        if (!isVerified)
            return UserErrors.PasswordInvalid();

        if (await repository.HasWithEmail(command.NewEmail, ct))
            return UserErrors.EmailDublicate(command.NewEmail);

        user.UpdateLoginDate();
        var request = user.ChangeEmail(command.NewEmail);
        if (request.IsFailure)
            return request.Error;

        await repository.Update(request.Value, ct);
        return request;
    }
}
