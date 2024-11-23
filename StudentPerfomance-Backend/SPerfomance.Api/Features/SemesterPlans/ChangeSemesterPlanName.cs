using System.Text;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.EducationPlans.Contracts;
using SPerfomance.Api.Features.SemesterPlans.Contracts;
using SPerfomance.Api.Features.Semesters.Contracts;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;
using SPerfomance.Application.EducationPlans.Queries.GetEducationPlan;
using SPerfomance.Application.Semesters.Commands.ChangeDisciplineName;
using SPerfomance.Application.Semesters.DTO;
using SPerfomance.Application.Semesters.Queries.GetDisciplineFromSemester;
using SPerfomance.Application.Semesters.Queries.GetSemester;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.Semesters;

namespace SPerfomance.Api.Features.SemesterPlans;

public static class ChangeSemesterPlanName
{
    public record Request(
        GetEducationDirectionQuery Direction,
        EducationPlanContract Plan,
        SemesterContract Semester,
        SemesterPlanContract Initial,
        SemesterPlanContract Updated
    );

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPut($"{SemesterPlanTags.Api}", Handler)
                .WithTags(SemesterPlanTags.Tag)
                .WithOpenApi()
                .WithName("ChangeSemesterPlanName")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод изменяет название дисциплины")
                        .AppendLine("Результат ОК (200): Дисциплина с измененным названием.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine("Результат Ошибки (404): Дисциплина не найдена")
                        .ToString()
                );
    }

    public static async Task<IResult> Handler(
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

        var plan = await queryDispatcher.Dispatch<GetEducationPlanQuery, EducationPlan>(
            new GetEducationPlanQuery(direction.Value, request.Plan.PlanYear),
            ct
        );

        if (plan.IsFailure)
            return TypedResults.NotFound(plan.Error.Description);

        var semester = await queryDispatcher.Dispatch<GetSemesterQuery, Semester>(
            new GetSemesterQuery(plan.Value, request.Semester.Number),
            ct
        );

        if (semester.IsFailure)
            return TypedResults.NotFound(semester.Error.Description);

        var discipline = await queryDispatcher.Dispatch<
            GetDisciplineFromSemesterQuery,
            SemesterPlan
        >(new GetDisciplineFromSemesterQuery(semester.Value, request.Initial.Discipline), ct);

        if (discipline.IsFailure)
            return TypedResults.NotFound(discipline.Error.Description);

        discipline = await commandDispatcher.Dispatch<ChangeDisciplineNameCommand, SemesterPlan>(
            new ChangeDisciplineNameCommand(discipline.Value, request.Updated.Discipline),
            ct
        );

        return discipline.IsFailure
            ? TypedResults.BadRequest(discipline.Error.Description)
            : TypedResults.Ok(discipline.Value.MapFromDomain());
    }
}
