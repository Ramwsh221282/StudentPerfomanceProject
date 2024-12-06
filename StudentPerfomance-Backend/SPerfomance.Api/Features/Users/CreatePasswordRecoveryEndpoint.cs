using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.PasswordRecoveryContext.Commands.RegisterPasswordRecovery;
using SPerfomance.Application.PasswordRecoveryContext.Models;
using SPerfomance.Application.Services.Mailing;
using SPerfomance.Application.Services.Mailing.MailingMessages;

namespace SPerfomance.Api.Features.Users;

public class CreatePasswordRecoveryEndpoint
{
    public record Request(RegisterPasswordRecoveryCommand Command);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPut($"{UserTags.App}/password-recovery", Handler)
                .WithTags($"{UserTags.Tag}")
                .RequireRateLimiting("fixed")
                .RequireCors("Frontend");
    }

    public static async Task<Results<BadRequest<string>, Ok<bool>>> Handler(
        [FromBody] Request request,
        ICommandDispatcher dispatcher,
        ILogger<Endpoint> logger,
        IMailingService mailing,
        IConfiguration configuration,
        CancellationToken ct
    )
    {
        logger.LogInformation("Запрос на восстановление пароля");
        var origins = configuration.GetSection("Cors:Origins").Get<string[]>();
        var frontendUrl = configuration
            .GetSection("PasswordRecoveryFrontend:Frontend")
            .Get<string[]>();

        if (frontendUrl == null || frontendUrl.Length == 0)
        {
            logger.LogError("Не удалось получить значение FrontendUrl из конфигурации");
            return TypedResults.BadRequest(
                "Не удалось обработать запрос. Повторите попытку позже."
            );
        }
        var frontendRecoveryUrl = frontendUrl[0];

        if (origins == null || origins.Length == 0)
        {
            logger.LogError("Не удалось получить значение Origins из конфигурации");
            return TypedResults.BadRequest(
                "Не удалось обработать запрос. Повторите попытку позже."
            );
        }

        var baseUrl = origins[0];
        if (string.IsNullOrEmpty(baseUrl))
        {
            logger.LogError("Не удалось получить базовый URL");
            return TypedResults.BadRequest(
                "Не удалось обработать запрос. Повторите попытку позже."
            );
        }

        if (string.IsNullOrEmpty(baseUrl))
        {
            logger.LogError("Не удалось получить базовый URL");
            return TypedResults.BadRequest(
                "Не удалось обработать запрос. Повторите попытку позже."
            );
        }

        var ticket = await dispatcher.Dispatch<
            RegisterPasswordRecoveryCommand,
            PasswordRecoveryTicket
        >(request.Command, ct);
        if (ticket.IsFailure)
        {
            logger.LogError(
                "Запрос на восстановление пароля отменен. Причина {text}",
                ticket.Error.Description
            );
            return TypedResults.BadRequest(ticket.Error.Description);
        }

        string recoveryLink = $"{frontendRecoveryUrl}/reset-password?token={ticket.Value.Token}";
        StringBuilder messageBuilder = new StringBuilder();
        messageBuilder.AppendLine("Запрос на восстановление пароля принят.");
        messageBuilder.AppendLine("Для восстановления пароля перейдите по ссылке:");
        messageBuilder.AppendLine(recoveryLink);
        messageBuilder.AppendLine(
            "Если вы не запрашивали восстановление пароля, проигнорируйте это сообщение."
        );

        MailingMessage message = new PasswordRecoveryMailingMessage(
            request.Command.Email!,
            messageBuilder.ToString()
        );
        var sending = mailing.SendMessage(message);
        logger.LogInformation("Сформирована ссылка на восстановление пароля: {link}", recoveryLink);
        logger.LogInformation(
            "Отправлен запрос на восстановление пароля {email}",
            request.Command.Email
        );
        return TypedResults.Ok(true);
    }
}
