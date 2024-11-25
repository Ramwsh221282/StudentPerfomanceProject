using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.PasswordRecoveryContext.Commands.ResolvePasswordRecovery;
using SPerfomance.Application.PasswordRecoveryContext.Models;
using SPerfomance.Application.Services.Mailing;
using SPerfomance.Application.Services.Mailing.MailingMessages;

namespace SPerfomance.Api.Features.Users;

public static class ConfirmPasswordRecoveryEndpoint
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet("reset-password", Handler).RequireRateLimiting("fixed");
    }

    public static async Task<Results<BadRequest<string>, Ok<bool>>> Handler(
        [FromQuery(Name = "token")] string token,
        ICommandDispatcher dispatcher,
        IMailingService service,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation("Подтверждение восстановления пароля пользователя");
        var command = new ResolvePasswordRecoveryCommand(token);
        var result = await dispatcher.Dispatch<
            ResolvePasswordRecoveryCommand,
            PasswordRecoveryTicket
        >(command, ct);
        if (result.IsFailure)
        {
            logger.LogError(
                "Подтверждение восстановления пароля пользователя отклонено. Причина {text}",
                result.Error.Description
            );
            return TypedResults.BadRequest(result.Error.Description);
        }

        logger.LogInformation("Восстановления пароля подтверждено {email}", result.Value.Email);
        return TypedResults.Ok(true);
    }
}
