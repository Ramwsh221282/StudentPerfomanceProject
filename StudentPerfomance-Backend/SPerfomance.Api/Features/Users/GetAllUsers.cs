using System.Text;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.Users.DTO;
using SPerfomance.Domain.Models.Users;

namespace SPerfomance.Api.Features.Users;

public static class GetAllUsers
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{UserTags.Api}/all", Handler)
                .WithTags($"{UserTags.Tag}")
                .WithOpenApi()
                .WithName("GetAllUsers")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод возвращает данные всех пользователей системы")
                        .AppendLine("Результат ОК (200): Список dto пользователей системы.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                )
                .RequireCors("Frontend");
    }

    public static async Task<IResult> Handler(
        [FromHeader(Name = "token")] string? token,
        IUsersRepository repository,
        CancellationToken ct = default
    )
    {
        Token jwt = new Token(token);
        if (!await jwt.IsVerifiedAdmin(repository))
            return TypedResults.Unauthorized();
        IReadOnlyCollection<User> users = await repository.GetAll();
        return Results.Ok(users.Select(u => u.MapFromDomain()));
    }
}
