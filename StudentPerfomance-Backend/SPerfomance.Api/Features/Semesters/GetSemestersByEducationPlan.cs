using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.EducationDirections.Contracts;
using SPerfomance.Api.Features.EducationPlans.Contracts;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;
using SPerfomance.Application.EducationPlans.Queries.GetEducationPlan;
using SPerfomance.Application.Semesters.DTO;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationPlans;

namespace SPerfomance.Api.Features.Semesters;

public static class GetSemestersByEducationPlan
{
    public record Request(
        EducationDirectionContract Direction,
        EducationPlanContract Plan,
        TokenContract Token
    );

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{SemestersTags.Api}", Handler)
                .WithOpenApi()
                .WithTags($"{SemestersTags.Tag}")
                .WithName("GetSemestersByEducationPlan")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод возвращает семестры учебного плана")
                        .AppendLine("Результат ОК (200): Список семестров учебного плана.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine("Результат Ошибки (404): Семестры не найдены.")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, NotFound<string>, Ok<IEnumerable<SemesterDto>>>
    > Handler(
        [FromHeader(Name = "token")] string token,
        [FromQuery(Name = "directionName")] string directionName,
        [FromQuery(Name = "directionCode")] string directionCode,
        [FromQuery(Name = "directionType")] string directionType,
        [FromQuery(Name = "planYear")] int planYear,
        IUsersRepository users,
        IQueryDispatcher dispatcher,
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerifiedAdmin(users, ct))
            return TypedResults.Unauthorized();

        var directionQuery = new GetEducationDirectionQuery(
            directionName,
            directionCode,
            directionType
        );
        var direction = await dispatcher.Dispatch<GetEducationDirectionQuery, EducationDirection>(
            directionQuery,
            ct
        );

        if (direction.IsFailure)
            return TypedResults.NotFound(direction.Error.Description);

        var planQuery = new GetEducationPlanQuery(direction.Value, planYear);
        var plan = await dispatcher.Dispatch<GetEducationPlanQuery, EducationPlan>(planQuery, ct);

        if (plan.IsFailure)
            return TypedResults.NotFound(plan.Error.Description);

        IEnumerable<SemesterDto> semesters = plan
            .Value.Semesters.Select(s => s.MapFromDomain())
            .OrderBy(s => s.Number);

        return TypedResults.Ok(semesters);
    }
}
