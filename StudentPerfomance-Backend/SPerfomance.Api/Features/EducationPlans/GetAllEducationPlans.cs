using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.EducationPlans.DTO;
using SPerfomance.Domain.Models.EducationPlans.Abstractions;

namespace SPerfomance.Api.Features.EducationPlans;

public static class GetAllEducationPlans
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{EducationPlanTags.Api}/all", Handler)
                .WithTags(EducationPlanTags.Tag)
                .WithOpenApi()
                .WithName("GetAllEducationPlans")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод возвращает все учебные планы постранично")
                        .AppendLine("Результат ОК (200): учебные планы постранично.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                )
                .RequireCors("Frontend");
    }

    public static async Task<
        Results<UnauthorizedHttpResult, Ok<IEnumerable<EducationPlanDto>>>
    > Handler(
        [FromHeader(Name = "token")] string? token,
        IUsersRepository users,
        IEducationPlansRepository repository,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation("Запрос на получение всех учебных планов");
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedAdmin(users, ct))
        {
            logger.LogError("Пользователь не является администратором");
            return TypedResults.Unauthorized();
        }
        var plans = await repository.GetAll(ct);
        logger.LogInformation(
            "Пользователь {id} получает все учебные планы в количестве {count}",
            jwtToken.UserId,
            plans.Count
        );
        return TypedResults.Ok(plans.Select(p => p.MapFromDomain()));
    }
}
