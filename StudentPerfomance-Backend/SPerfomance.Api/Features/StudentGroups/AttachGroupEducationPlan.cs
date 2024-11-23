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
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, NotFound<string>, BadRequest<string>, Ok<StudentGroupDto>>
    > Handler(
        [FromBody] Request request,
        [FromHeader(Name = "token")] string token,
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

        var plan = await queryDispatcher.Dispatch<GetEducationPlanQuery, EducationPlan>(
            new GetEducationPlanQuery(direction.Value, request.Plan.PlanYear),
            ct
        );

        if (plan.IsFailure)
            return TypedResults.NotFound(plan.Error.Description);

        var group = await queryDispatcher.Dispatch<GetStudentGroupQuery, StudentGroup>(
            request.Group,
            ct
        );
        if (group.IsFailure)
            return TypedResults.NotFound(group.Error.Description);

        group = await commandDispatcher.Dispatch<AttachEducationPlanCommand, StudentGroup>(
            new AttachEducationPlanCommand(plan.Value, group.Value, request.Semester.Number),
            ct
        );

        if (group.IsFailure)
            return TypedResults.NotFound(group.Error.Description);

        return group.IsFailure
            ? TypedResults.BadRequest(group.Error.Description)
            : TypedResults.Ok(group.Value.MapFromDomain());
    }
}
