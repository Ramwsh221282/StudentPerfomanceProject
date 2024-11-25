using SPerfomance.Application.Abstractions;
using SPerfomance.Application.PasswordRecoveryContext.Models;
using SPerfomance.Application.PasswordRecoveryContext.Models.Errors;
using SPerfomance.Application.PasswordRecoveryContext.RepositoryAbstraction;
using SPerfomance.Application.Services.Authentication.Abstractions;
using SPerfomance.Domain.Models.Users.Abstractions;
using SPerfomance.Domain.Models.Users.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.PasswordRecoveryContext.Commands.RegisterPasswordRecovery;

public sealed class RegisterPasswordRecoveryCommandHandler(
    IPasswordRecoveryTicketsRepository tickets,
    IUsersRepository users,
    IJwtTokenService tokens
) : ICommandHandler<RegisterPasswordRecoveryCommand, PasswordRecoveryTicket>
{
    public async Task<Result<PasswordRecoveryTicket>> Handle(
        RegisterPasswordRecoveryCommand command,
        CancellationToken ct = default
    )
    {
        if (string.IsNullOrWhiteSpace(command.Email))
            return PasswordRecoveryErrors.EmailWasEmpty;

        var user = await users.GetByEmail(command.Email, ct);
        if (user is null)
            return UserErrors.NotFound();

        var token = tokens.GenerateRecoveryTicket(user);
        var ticket = new PasswordRecoveryTicket() { Email = command.Email, Token = token };
        await tickets.RegisterTicket(ticket, ct);
        return ticket;
    }
}
