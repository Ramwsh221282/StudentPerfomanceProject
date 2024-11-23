using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.EducationPlans.Contracts;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;
using SPerfomance.Application.EducationPlans.Commands.ChangeEducationPlanYear;
using SPerfomance.Application.EducationPlans.DTO;
using SPerfomance.Application.EducationPlans.Queries.GetEducationPlan;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationPlans;

namespace SPerfomance.Api.Features.EducationPlans;

public static class ChangeEducationPlanYear
{
    public record Request(
        GetEducationDirectionQuery Direction,
        EducationPlanContract Initial,
        EducationPlanContract Updated
    );

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPut($"{EducationPlanTags.Api}", Handler)
                .WithTags(EducationPlanTags.Tag)
                .WithOpenApi()
                .WithName("ChangeEducationPlanYear")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод изменяет год направления подготовки в системе")
                        .AppendLine(
                            "Результат ОК (200): Направление подготовки с измененным годом."
                        )
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine("Результат Ошибки (404): Не найдено направление подготовки.")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, NotFound<string>, BadRequest<string>, Ok<EducationPlanDto>>
    > Handler(
        [FromHeader(Name = "token")] string token,
        [FromBody] Request request,
        IUsersRepository users,
        IQueryDispatcher queryDispatcher,
        ICommandDispatcher commandDispatcher,
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerifiedAdmin(users, ct))
            return TypedResults.Unauthorized();

        var direction = await queryDispatcher.Dispatch<
            GetEducationDirectionQuery,
            EducationDirection
        >(request.Direction, ct);

        if (direction.IsFailure)
            return TypedResults.NotFound(direction.Error.Description);

        var educationPlan = await queryDispatcher.Dispatch<GetEducationPlanQuery, EducationPlan>(
            new GetEducationPlanQuery(direction.Value, request.Initial.PlanYear),
            ct
        );

        if (educationPlan.IsFailure)
            return TypedResults.NotFound(educationPlan.Error.Description);

        educationPlan = await commandDispatcher.Dispatch<
            ChangeEducationPlanYearCommand,
            EducationPlan
        >(new ChangeEducationPlanYearCommand(educationPlan.Value, request.Updated.PlanYear), ct);

        return educationPlan.IsFailure
            ? TypedResults.BadRequest(educationPlan.Error.Description)
            : TypedResults.Ok(educationPlan.Value.MapFromDomain());
    }
}
