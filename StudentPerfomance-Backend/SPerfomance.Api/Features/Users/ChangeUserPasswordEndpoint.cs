using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.Users.Commands.ChangePassword;
using SPerfomance.Domain.Models.Users;

namespace SPerfomance.Api.Features.Users;

public static class ChangeUserPasswordEndpoint
{
    public record Request(ChangePasswordCommand Command);

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
            app.MapPut($"{UserTags.App}/password", Handler)
                .WithTags(UserTags.Tag)
                .WithOpenApi()
                .WithName("UsersPasswordUpdate")
                .RequireRateLimiting("fixed")
                .WithDescription(new StringBuilder().AppendLine("Изменение пароля").ToString())
                .RequireCors("Frontend");
    }

    public static async Task<Results<BadRequest<string>, Ok<Response>>> Handler(
        [FromBody] Request request,
        ICommandDispatcher dispatcher,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation("Запрос на изменения пароля пользователя");
        var result = await dispatcher.Dispatch<ChangePasswordCommand, User>(request.Command, ct);
        if (result.IsFailure)
        {
            logger.LogError(
                "Запрос на изменением пароля отменён. Причина: {text}",
                result.Error.Description
            );
            return TypedResults.BadRequest(result.Error.Description);
        }

        var responseResult = new Response(
            result.Value.Name.Name,
            result.Value.Name.Surname,
            result.Value.Name.Patronymic,
            result.Value.Email.Email,
            result.Value.Role.Role,
            request.Command.Token!
        );

        return TypedResults.Ok(responseResult);
    }
}
