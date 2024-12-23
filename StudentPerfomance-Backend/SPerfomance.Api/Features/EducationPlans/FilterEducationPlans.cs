using System.Text;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.EducationPlans.DTO;
using SPerfomance.Domain.Models.EducationPlans.Abstractions;

namespace SPerfomance.Api.Features.EducationPlans;

public static class FilterEducationPlans
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{EducationPlanTags.Api}/filter", Handler)
                .WithTags(EducationPlanTags.Tag)
                .WithOpenApi()
                .WithName("FilterEducationPlans")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод возвращает отфильтрованные учебные планы постранично")
                        .AppendLine(
                            "Результат ОК (200): Отфильтрованные учебные планы постранично."
                        )
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                )
                .RequireCors("Frontend");
    }

    public static async Task<IResult> Handler(
        [FromHeader(Name = "token")] string? token,
        [FromQuery(Name = "page")] int page,
        [FromQuery(Name = "pageSize")] int pageSize,
        [FromQuery(Name = "filterName")] string? filterName,
        [FromQuery(Name = "filterCode")] string? filterCode,
        [FromQuery(Name = "filterType")] string? filterType,
        [FromQuery(Name = "filterYear")] int? filterYear,
        IUsersRepository users,
        IEducationPlansRepository repository,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation("Запрос на фильтр учебных планов постранично");
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedAdmin(users, ct))
        {
            logger.LogError("Пользователь не является администратором");
            return TypedResults.BadRequest(UserTags.UnauthorizedError);
        }

        var plans = await repository.GetPagedFiltered(
            filterName,
            filterCode,
            filterType,
            filterYear,
            page,
            pageSize,
            ct
        );
        logger.LogInformation(
            "Пользователь {id} получает отфильтрованные постранично учебные планы в количестве {count}",
            jwtToken.UserId,
            plans.Count
        );
        return TypedResults.Ok(plans.Select(p => p.MapFromDomain()));
    }
}
