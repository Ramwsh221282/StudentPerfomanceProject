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
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Queries.HasActive;
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
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод создаёт учебный план")
                        .AppendLine("Результат ОК (200): Созданный учебный план.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine("Результат Ошибки (404): Направление подготовки не найдено.")
                        .ToString()
                )
                .RequireCors("Frontend");
    }

    public static async Task<
        Results<BadRequest<string>, UnauthorizedHttpResult, NotFound<string>, Ok<EducationPlanDto>>
    > Handler(
        [FromHeader(Name = "token")] string? token,
        Request request,
        IUsersRepository users,
        HasActiveAssignmentSessionRequestHandler guard,
        ICommandDispatcher commandDispatcher,
        IQueryDispatcher queryDispatcher,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        HasActiveAssignmentSessionResponse response = await guard.Handle(
            new HasActiveAssignmentSessionRequest()
        );
        if (response.Has)
            return TypedResults.BadRequest("Запрос отклонён. Причина: Активная контрольная неделя");

        logger.LogInformation("Запрос на создание учебного плана");
        var jwtToken = new Token(token);
        if (!await new Token(token).IsVerifiedAdmin(users, ct))
        {
            logger.LogError("Пользователь не является администратором");
            return TypedResults.Unauthorized();
        }

        var direction = await queryDispatcher.Dispatch<
            GetEducationDirectionQuery,
            EducationDirection
        >(request.Direction, ct);

        if (direction.IsFailure)
        {
            logger.LogError("{text}", direction.Error.Description);
            return TypedResults.Unauthorized();
        }

        var plan = await commandDispatcher.Dispatch<CreateEducationPlanCommand, EducationPlan>(
            new CreateEducationPlanCommand(direction.Value, request.Plan.PlanYear),
            ct
        );

        if (plan.IsFailure)
        {
            logger.LogError("{text}", plan.Error.Description);
            return TypedResults.BadRequest(plan.Error.Description);
        }

        logger.LogInformation(
            "Пользователь {id} создает учебный план {year} направления {code} {name} {type}",
            jwtToken.UserId,
            plan.Value.Year,
            plan.Value.Direction.Code.Code,
            plan.Value.Direction.Name.Name,
            plan.Value.Direction.Type.Type
        );
        return TypedResults.Ok(plan.Value.MapFromDomain());
    }
}
