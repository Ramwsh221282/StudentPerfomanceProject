using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.Users.Commands.UpdateUser;
using SPerfomance.Application.Users.DTO;
using SPerfomance.Domain.Models.Users;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Api.Features.Users;

public static class UpdateUser
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPut($"{UserTags.Api}", Handler)
                .WithTags(UserTags.Tag)
                .WithOpenApi()
                .WithName("UpdateUser")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод изменяет данные пользователя")
                        .AppendLine(
                            "Результат ОК (200): Возвращает измененные данные пользователя."
                        )
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                )
                .RequireCors("Frontend");
    }

    public static async Task<
        Results<UnauthorizedHttpResult, BadRequest<string>, Ok<UserDto>>
    > Handler(
        [FromHeader(Name = "token")] string? token,
        [FromBody] UpdateUserCommand request,
        IUsersRepository repository,
        ICommandDispatcher dispatcher,
        CancellationToken ct
    )
    {
        Token jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedAdmin(repository))
            return TypedResults.Unauthorized();
        Result<User> result = await dispatcher.Dispatch<UpdateUserCommand, User>(request, ct);
        return result.IsFailure
            ? TypedResults.BadRequest(result.Error.Description)
            : TypedResults.Ok(result.Value.MapFromDomain());
    }
}
