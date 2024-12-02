using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.Users.Contracts;
using SPerfomance.Application.Services.Mailing;
using SPerfomance.Application.Services.Mailing.MailingMessages;
using SPerfomance.Application.Users.Commands.RemoveUser;
using SPerfomance.Application.Users.DTO;
using SPerfomance.Application.Users.Queries.GetUserByEmail;

namespace SPerfomance.Api.Features.Users;

public static class RemoveUser
{
    public record Request(UserContract User, TokenContract Token);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapDelete($"{UserTags.Api}", Handler)
                .WithTags(UserTags.Tag)
                .WithOpenApi()
                .WithName("RemoveUser")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод удаляет пользователя из системы")
                        .AppendLine("Результат ОК (200): Возвращает удаленного пользователя.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine("Результат Ошибки (404): Пользователь не найден не найден")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, BadRequest<string>, NotFound<string>, Ok<UserDto>>
    > Handler(
        [FromHeader(Name = "token")] string? token,
        [FromBody] Request request,
        IUsersRepository repository,
        IMailingService service,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation("Запрос на удаление пользователя из системы");
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedAdmin(repository, ct))
        {
            logger.LogError("Пользователь не является администратором");
            return TypedResults.Unauthorized();
        }

        var user = await new GetUserByEmailQueryHandler(repository).Handle(request.User, ct);

        if (
            string.Equals(
                user.Value.Id.ToString(),
                jwtToken.UserId,
                StringComparison.OrdinalIgnoreCase
            )
        )
        {
            logger.LogError(
                "Запрос пользователя {id} на удаление пользователя из системы отменен. Нельзя удалить самого себя",
                jwtToken.UserId
            );
            return TypedResults.BadRequest("Вы не можете удалить самого себя из системы");
        }

        user = await new RemoveUserCommandHandler(repository).Handle(
            new RemoveUserCommand(user.Value),
            ct
        );

        if (user.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на удаление пользователя из системы отменен. Причина: {text}",
                jwtToken.UserId,
                user.Error.Description
            );
            return TypedResults.NotFound(user.Error.Description);
        }

        MailingMessage message = new UserRemoveMessage(user.Value.Email.Email);
        var sending = service.SendMessage(message);
        logger.LogInformation(
            "Пользователь {id} удаляет пользователя {uid} {uemail} {urole}",
            jwtToken.UserId,
            user.Value.Id,
            user.Value.Email.Email,
            user.Value.Role.Role
        );
        return TypedResults.Ok(user.Value.MapFromDomain());
    }
}
