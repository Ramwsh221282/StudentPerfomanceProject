using System.Text;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Domain.Models.EducationPlans.Abstractions;

namespace SPerfomance.Api.Features.EducationPlans;

public static class CountEducationPlans
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{EducationPlanTags.Api}/count", Handler)
                .WithTags(EducationPlanTags.Tag)
                .WithOpenApi()
                .WithName("CountEducationPlans")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine(
                            "Метод возвращает текущее количество направлений подготовки в системе"
                        )
                        .AppendLine("Результат ОК (200): Количество направлений подготовки.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                )
                .RequireCors("Frontend");
    }

    public static async Task<IResult> Handler(
        [FromHeader(Name = "token")] string? token,
        IUsersRepository users,
        IEducationPlansRepository repository,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedAdmin(users, ct))
        {
            logger.LogInformation("Пользователь не является администратором");
            return Results.BadRequest(UserTags.UnauthorizedError);
        }
        var count = await repository.Count(ct);
        logger.LogInformation(
            "Пользователь {id} получает количество учебных планов {count}",
            jwtToken.UserId,
            count
        );
        return Results.Ok(count);
    }
}
