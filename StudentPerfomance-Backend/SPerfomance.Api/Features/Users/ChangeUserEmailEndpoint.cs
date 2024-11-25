using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.Users.Commands.ChangeEmail;
using SPerfomance.Domain.Models.Users;

namespace SPerfomance.Api.Features.Users;

public static class ChangeUserEmailEndpoint
{
    public record Request(ChangeEmailCommand Command);

    public record Response(
        string Name,
        string Surname,
        string? Patronymic,
        string Email,
        string Role,
        string Token
    );

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPut($"{UserTags.App}/email", Handler)
                .WithTags(UserTags.Tag)
                .WithOpenApi()
                .WithName("UsersEmailUpdate")
                .RequireRateLimiting("fixed")
                .WithDescription(new StringBuilder().AppendLine("Изменение почты").ToString());
    }

    public static async Task<Results<BadRequest<string>, Ok<Response>>> Handler(
        [FromBody] Request request,
        ICommandDispatcher dispatcher,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation("Запрос на изменение почты пользователя");
        var result = await dispatcher.Dispatch<ChangeEmailCommand, User>(request.Command, ct);
        if (result.IsFailure)
        {
            logger.LogError(
                "Ошибка изменения почты пользователя. {text}",
                result.Error.Description
            );
            return TypedResults.BadRequest(result.Error.Description);
        }
        logger.LogInformation(
            "Пользователь {id} {uname} поменял почту с {oldEmail} на {newEmail}",
            result.Value.Id,
            result.Value.Name.Name,
            request.Command.CurrentEmail,
            request.Command.NewEmail
        );

        var response = new Response(
            result.Value.Name.Name,
            result.Value.Name.Surname,
            result.Value.Name.Patronymic,
            result.Value.Email.Email,
            result.Value.Role.Role,
            request.Command.Token!
        );
        return TypedResults.Ok(response);
    }
}
