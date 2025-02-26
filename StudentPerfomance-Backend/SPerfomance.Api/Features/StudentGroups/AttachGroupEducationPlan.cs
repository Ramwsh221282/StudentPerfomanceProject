using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.EducationPlans.Contracts;
using SPerfomance.Api.Features.Semesters.Contracts;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;
using SPerfomance.Application.EducationPlans.Queries.GetEducationPlan;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Queries.HasActive;
using SPerfomance.Application.StudentGroups.Commands.AttachEducationPlan;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Application.StudentGroups.Queries.GetStudentGroupByName;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Models.StudentGroups;

namespace SPerfomance.Api.Features.StudentGroups;

public static class AttachGroupEducationPlan
{
    public record Request(
        GetEducationDirectionQuery Direction,
        EducationPlanContract Plan,
        SemesterContract Semester,
        GetStudentGroupQuery Group
    );

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPut($"{StudentGroupTags.Api}/attach-education-plan", Handler)
                .WithTags($"{StudentGroupTags.Tag}")
                .WithOpenApi()
                .WithName("AttachGroupEducationPlan")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод закрепляет учебный план в группе")
                        .AppendLine(
                            "Результат ОК (200): Возвращает учебную группу с закрепленным учебным планов."
                        )
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine("Результат Ошибки (404: Учебный план или группа не найдены")
                        .ToString()
                )
                .RequireCors("Frontend");
    }

    public static async Task<
        Results<UnauthorizedHttpResult, NotFound<string>, BadRequest<string>, Ok<StudentGroupDto>>
    > Handler(
        [FromBody] Request request,
        [FromHeader(Name = "token")] string? token,
        HasActiveAssignmentSessionRequestHandler guard,
        IUsersRepository users,
        IQueryDispatcher queryDispatcher,
        ICommandDispatcher commandDispatcher,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        HasActiveAssignmentSessionResponse response = await guard.Handle(
            new HasActiveAssignmentSessionRequest()
        );
        if (response.Has)
            return TypedResults.BadRequest("Запрос отклонён. Причина: Активная контрольная неделя");

        logger.LogInformation("Запрос на закрепление учебного плана для группы");
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
                "Запрос пользователя {id} на закрепление учебного плана для группы отмёнен. Причина: {text}",
                jwtToken.UserId,
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
                "Запрос пользователя {id} на закрепление учебного плана для группы отмёнен. Причина: {text}",
                jwtToken.UserId,
                plan.Error.Description
            );
            return TypedResults.NotFound(plan.Error.Description);
        }

        var group = await queryDispatcher.Dispatch<GetStudentGroupQuery, StudentGroup>(
            request.Group,
            ct
        );
        if (group.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на закрепление учебного плана для группы отмёнен. Причина: {text}",
                jwtToken.UserId,
                group.Error.Description
            );
            return TypedResults.NotFound(group.Error.Description);
        }

        group = await commandDispatcher.Dispatch<AttachEducationPlanCommand, StudentGroup>(
            new AttachEducationPlanCommand(plan.Value, group.Value, request.Semester.Number),
            ct
        );

        if (group.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на закрепление учебного плана для группы отмёнен. Причина: {text}",
                jwtToken.UserId,
                group.Error.Description
            );
            return TypedResults.NotFound(group.Error.Description);
        }

        logger.LogInformation(
            "Пользователь {id} закрепляет учебный план {planYear} {code} {dname} {dtype} группе {gname}",
            jwtToken.UserId,
            request.Plan.PlanYear,
            request.Direction.Code,
            request.Direction.Name,
            request.Direction.Type,
            request.Group.Name
        );
        return TypedResults.Ok(group.Value.MapFromDomain());
    }
}
