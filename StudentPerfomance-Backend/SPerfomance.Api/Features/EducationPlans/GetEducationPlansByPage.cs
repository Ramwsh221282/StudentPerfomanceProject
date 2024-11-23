using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.EducationPlans.DTO;
using SPerfomance.Domain.Models.EducationPlans.Abstractions;

namespace SPerfomance.Api.Features.EducationPlans;

public static class GetEducationPlansByPage
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{EducationPlanTags.Api}", Handler)
                .WithTags(EducationPlanTags.Tag)
                .WithOpenApi()
                .WithName("GetEducationPlansByPage")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод возвращает учебные планы постранично")
                        .AppendLine("Результат ОК (200): учебные планы постранично.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, Ok<IEnumerable<EducationPlanDto>>>
    > Handler(
        [FromHeader(Name = "token")] string token,
        [FromQuery(Name = "page")] int page,
        [FromQuery(Name = "pageSize")] int pageSize,
        IUsersRepository users,
        IEducationPlansRepository repository,
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerifiedAdmin(users, ct))
            return TypedResults.Unauthorized();
        var plans = await repository.GetPaged(page, pageSize, ct);
        return TypedResults.Ok(plans.Select(p => p.MapFromDomain()));
    }
}
