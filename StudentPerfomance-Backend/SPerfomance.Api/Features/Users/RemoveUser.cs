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
        Results<UnauthorizedHttpResult, NotFound<string>, Ok<UserDto>>
    > Handler(
        [FromHeader(Name = "token")] string token,
        [FromBody] Request request,
        IUsersRepository repository,
        IMailingService service,
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerifiedAdmin(repository, ct))
            return TypedResults.Unauthorized();

        var user = await new GetUserByEmailQueryHandler(repository).Handle(request.User, ct);
        user = await new RemoveUserCommandHandler(repository).Handle(
            new RemoveUserCommand(user.Value),
            ct
        );

        if (user.IsFailure)
            return TypedResults.NotFound(user.Error.Description);

        MailingMessage message = new UserRemoveMessage(user.Value.Email.Email);
        var sending = service.SendMessage(message);
        return TypedResults.Ok(user.Value.MapFromDomain());
    }
}
