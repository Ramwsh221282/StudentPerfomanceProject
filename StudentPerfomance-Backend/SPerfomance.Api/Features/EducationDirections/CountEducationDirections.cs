using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;

namespace SPerfomance.Api.Features.EducationDirections;

public static class CountEducationDirections
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{EducationDirectionTags.Api}/count", Handler)
                .WithTags(EducationDirectionTags.Tag)
                .WithOpenApi()
                .WithName("GetEducationDirectionsCount")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine(
                            "Метод возвращает количество существующих направлений подготовки из базы данных"
                        )
                        .AppendLine("Результат ОК (200): целое число.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                )
                .RequireCors("Frontend");
    }

    public static async Task<Results<Ok<int>, UnauthorizedHttpResult>> Handler(
        [FromHeader(Name = "token")] string? token,
        IUsersRepository users,
        IEducationDirectionRepository repository,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation("Запрос на получение количества направлений подготовки");
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedAdmin(users, ct))
        {
            logger.LogError("Пользователь не является администратором");
            return TypedResults.Unauthorized();
        }
        var count = await repository.Count(ct);
        logger.LogInformation("Количество направлений подготовки: {Count}", count);
        return TypedResults.Ok(count);
    }
}
