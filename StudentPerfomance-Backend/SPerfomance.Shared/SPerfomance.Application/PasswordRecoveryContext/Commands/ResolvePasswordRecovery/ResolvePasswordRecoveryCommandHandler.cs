using System.Text;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.PasswordRecoveryContext.Models;
using SPerfomance.Application.PasswordRecoveryContext.Models.Errors;
using SPerfomance.Application.PasswordRecoveryContext.RepositoryAbstraction;
using SPerfomance.Application.Services.Authentication.Abstractions;
using SPerfomance.Application.Services.Mailing;
using SPerfomance.Application.Services.Mailing.MailingMessages;
using SPerfomance.Domain.Models.Users.Abstractions;
using SPerfomance.Domain.Models.Users.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.PasswordRecoveryContext.Commands.ResolvePasswordRecovery;

public sealed class ResolvePasswordRecoveryCommandHandler(
    IPasswordRecoveryTicketsRepository tickets,
    IUsersRepository users,
    IPasswordGenerator generator,
    IPasswordHasher hasher,
    IMailingService service
) : ICommandHandler<ResolvePasswordRecoveryCommand, PasswordRecoveryTicket>
{
    public async Task<Result<PasswordRecoveryTicket>> Handle(
        ResolvePasswordRecoveryCommand command,
        CancellationToken ct = default
    )
    {
        if (string.IsNullOrWhiteSpace(command.Token))
            return PasswordRecoveryErrors.TokenWasNotFound;

        var ticket = await tickets.FindRecoveryTicket(command.Token, ct);
        if (ticket is null)
            return PasswordRecoveryErrors.TokenWasNotFound;

        var user = await users.GetByEmail(ticket.Email, ct);
        if (user is null)
            return UserErrors.NotFound();

        var newPassword = generator.Generate();
        var hashedPassword = hasher.Hash(newPassword);
        user.UpdatePassword(hashedPassword);
        await users.Update(user, ct);
        await tickets.DeleteTicket(ticket, ct);

        var content = new StringBuilder();
        content.AppendLine("Ваш новый пароль для авторизации на SPerfomance:");
        content.AppendLine(newPassword);

        MailingMessage message = new PasswordResetMailingMessage(ticket.Email, content.ToString());
        var sending = service.SendMessage(message);

        return ticket;
    }
}
