using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.EducationPlans.Contracts;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;
using SPerfomance.Application.EducationPlans.Commands.CreateEducationPlan;
using SPerfomance.Application.EducationPlans.DTO;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationPlans;

namespace SPerfomance.Api.Features.EducationPlans;

public static class CreateEducationPlan
{
    public record Request(GetEducationDirectionQuery Direction, EducationPlanContract Plan);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{EducationPlanTags.Api}", Handler)
                .WithTags(EducationPlanTags.Tag)
                .WithOpenApi()
                .WithName("CreateEducationPlan")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод создаёт учебный план")
                        .AppendLine("Результат ОК (200): Созданный учебный план.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine("Результат Ошибки (404): Направление подготовки не найдено.")
                        .ToString()
                );
    }

    public static async Task<
        Results<BadRequest<string>, UnauthorizedHttpResult, NotFound<string>, Ok<EducationPlanDto>>
    > Handler(
        [FromHeader(Name = "token")] string token,
        Request request,
        IUsersRepository users,
        ICommandDispatcher commandDispatcher,
        IQueryDispatcher queryDispatcher,
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

        var plan = await commandDispatcher.Dispatch<CreateEducationPlanCommand, EducationPlan>(
            new CreateEducationPlanCommand(direction.Value, request.Plan.PlanYear),
            ct
        );

        return plan.IsFailure
            ? TypedResults.BadRequest(plan.Error.Description)
            : TypedResults.Ok(plan.Value.MapFromDomain());
    }
}
