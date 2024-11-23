using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;
using SPerfomance.Application.EducationPlans.DTO;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationDirections.Errors;

namespace SPerfomance.Api.Features.EducationPlans;

public static class GetEducationPlansByDirection
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{EducationPlanTags.Api}/by-education-direction", Handler)
                .WithTags($"{EducationPlanTags.Tag}")
                .WithOpenApi()
                .WithName("GetEducationPlansByDirection")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine(
                            "Метод возвращает все учебные планы указанного направления подготовки"
                        )
                        .AppendLine(
                            "Результат ОК (200): учебные планы указанного направления подготовки."
                        )
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, NotFound<string>, Ok<IEnumerable<EducationPlanDto>>>
    > Handler(
        [FromHeader(Name = "token")] string token,
        [FromQuery(Name = "directionCode")] string directionCode,
        [FromQuery(Name = "directionName")] string directionName,
        [FromQuery(Name = "directionType")] string directionType,
        IUsersRepository users,
        IQueryDispatcher dispatcher,
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerifiedAdmin(users, ct))
            return TypedResults.Unauthorized();

        var query = new GetEducationDirectionQuery(directionName, directionCode, directionType);
        var direction = await dispatcher.Dispatch<GetEducationDirectionQuery, EducationDirection>(
            query,
            ct
        );

        return direction == null || direction.IsFailure
            ? TypedResults.NotFound(EducationDirectionErrors.NotFoundError().Description)
            : TypedResults.Ok(direction.Value.Plans.Select(p => p.MapFromDomain()));
    }
}
