using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.EducationPlans.Contracts;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;
using SPerfomance.Application.EducationPlans.Commands.RemoveEducationPlan;
using SPerfomance.Application.EducationPlans.DTO;
using SPerfomance.Application.EducationPlans.Queries.GetEducationPlan;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationPlans;

namespace SPerfomance.Api.Features.EducationPlans;

public static class RemoveEducationPlan
{
    public record Request(GetEducationDirectionQuery Direction, EducationPlanContract Plan);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapDelete($"{EducationPlanTags.Api}", Handler)
                .WithTags(EducationPlanTags.Tag)
                .WithOpenApi()
                .WithName("RemoveEducationPlan")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод удаляет учебный план из направления подготовки")
                        .AppendLine("Результат ОК (200): Удаленный учебный план.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine("Результат Ошибки (404): Учебный план не найден.")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, NotFound<string>, BadRequest<string>, Ok<EducationPlanDto>>
    > Handler(
        [FromHeader(Name = "token")] string token,
        [FromBody] Request request,
        IUsersRepository users,
        ICommandDispatcher commandDispatcher,
        IQueryDispatcher queryDispatcher,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation("Запрос на удаление учебного плана");
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedAdmin(users, ct))
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
            logger.LogError(
                "Запрос на удаление учебного плана. Ошибка: {text}",
                direction.Error.Description
            );
            return TypedResults.NotFound(direction.Error.Description);
        }

        var plan = await queryDispatcher.Dispatch<GetEducationPlanQuery, EducationPlan>(
            new GetEducationPlanQuery(direction.Value, request.Plan.PlanYear),
            ct
        );

        if (plan.IsFailure)
        {
            logger.LogError(
                "Запрос на удаление учебного плана. Ошибка {text}",
                plan.Error.Description
            );
            return TypedResults.NotFound(plan.Error.Description);
        }

        plan = await commandDispatcher.Dispatch<RemoveEducationPlanCommand, EducationPlan>(
            new RemoveEducationPlanCommand(plan.Value),
            ct
        );

        if (plan.IsFailure)
        {
            logger.LogError(
                "Запрос на удаление учебного плана. Ошибка {text}",
                plan.Error.Description
            );
            return TypedResults.BadRequest(plan.Error.Description);
        }

        logger.LogInformation(
            "Пользователь {id} удаляет учебный план {year} {code} {name} {type}",
            jwtToken.UserId,
            plan.Value.Year.Year,
            plan.Value.Direction.Code.Code,
            plan.Value.Direction.Name.Name,
            plan.Value.Direction.Type.Type
        );
        return TypedResults.Ok(plan.Value.MapFromDomain());
    }
}
