using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.Common.Extensions;

namespace SPerfomance.Api.Features.Users;

public static class GetUsersCount
{
    public record Request(TokenContract Token);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{UserTags.Api}/count", Handler)
                .WithTags($"{UserTags.Tag}")
                .WithOpenApi()
                .WithName("GetUsersCount")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод возвращает количество пользователей")
                        .AppendLine("Результат ОК (200): Количество пользователей.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                )
                .RequireCors("Frontend");
    }

    public static async Task<Results<UnauthorizedHttpResult, Ok<int>>> Handler(
        [FromHeader(Name = "token")] string? token,
        IUsersRepository repository,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation("Получение количества пользователей в системе");
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedAdmin(repository, ct))
        {
            logger.LogError("Пользователь не является администратором");
            return TypedResults.Unauthorized();
        }

        var count = await repository.Count(ct);
        logger.LogInformation(
            "Пользователь {id} получает количество пользователей в системе {count}",
            jwtToken.UserId,
            count
        );
        return TypedResults.Ok(count);
    }
}
