using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.EducationPlans.DTO;
using SPerfomance.Domain.Models.EducationPlans.Abstractions;

namespace SPerfomance.Api.Features.EducationPlans;

public static class SearchEducationPlans
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{EducationPlanTags.Api}/search", Handler)
                .WithTags(EducationPlanTags.Tag)
                .WithOpenApi()
                .WithName("SearchEducationPlans")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод возвращает отфильтрованные учебные планы из всех")
                        .AppendLine("Результат ОК (200): Отфильтрованные учебные планы.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, Ok<IEnumerable<EducationPlanDto>>>
    > Handler(
        [FromHeader(Name = "token")] string token,
        [FromQuery(Name = "searchName")] string? searchName,
        [FromQuery(Name = "searchCode")] string? searchCode,
        [FromQuery(Name = "searchType")] string? searchType,
        [FromQuery(Name = "searchYear")] int? searchYear,
        IUsersRepository users,
        IEducationPlansRepository repository,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation("Запрос на поиск учебных планов");
        var jwtToken = new Token(token);
        if (!await new Token(token).IsVerifiedAdmin(users, ct))
        {
            logger.LogError("Пользователь не является администратором");
            return TypedResults.Unauthorized();
        }

        var plans = await repository.GetFiltered(
            searchName,
            searchCode,
            searchType,
            searchYear,
            ct
        );
        logger.LogInformation(
            "Пользователь {id} получает учебные планы в количестве {count}",
            jwtToken.UserId,
            plans.Count
        );
        return TypedResults.Ok(plans.Select(p => p.MapFromDomain()));
    }
}
