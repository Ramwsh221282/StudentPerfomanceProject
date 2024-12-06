using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;

namespace SPerfomance.Api.Features.StudentGroups;

public static class CountStudentGroups
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{StudentGroupTags.Api}/count", Handler)
                .WithTags(StudentGroupTags.Tag)
                .WithOpenApi()
                .WithName("CountStudentGroups")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод возвращает количество студенческих групп")
                        .AppendLine(
                            "Результат ОК (200): Возвращает студенческую группу с измененным названием."
                        )
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                )
                .RequireCors("Frontend");
    }

    public static async Task<Results<UnauthorizedHttpResult, Ok<int>>> Handler(
        [FromHeader(Name = "token")] string? token,
        IUsersRepository users,
        IStudentGroupsRepository repository,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation("Запрос на получение количества студенческих групп");
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedAdmin(users, ct))
        {
            logger.LogError("Пользователь не является администратором");
            return TypedResults.Unauthorized();
        }
        var count = await repository.Count(ct);
        logger.LogInformation(
            "Пользователь {id} получает количество студенческих групп {count}",
            jwtToken.UserId,
            count
        );
        return TypedResults.Ok(count);
    }
}
