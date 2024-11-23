using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.EducationPlans.Contracts;
using SPerfomance.Api.Features.SemesterPlans.Contracts;
using SPerfomance.Api.Features.Semesters.Contracts;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;
using SPerfomance.Application.EducationPlans.Queries.GetEducationPlan;
using SPerfomance.Application.Semesters.Commands.DeattachTeacherFromDiscipline;
using SPerfomance.Application.Semesters.DTO;
using SPerfomance.Application.Semesters.Queries.GetDisciplineFromSemester;
using SPerfomance.Application.Semesters.Queries.GetSemester;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.Semesters;

namespace SPerfomance.Api.Features.SemesterPlans;

public static class DeattachTeacher
{
    public record Request(
        GetEducationDirectionQuery Direction,
        EducationPlanContract Plan,
        SemesterContract Semester,
        SemesterPlanContract Discipline
    );

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPut($"{SemesterPlanTags.Api}/deattach-teacher", Handler)
                .WithTags(SemesterPlanTags.Tag)
                .WithOpenApi()
                .WithName("DeattachTeacher")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод открепляет дисциплину от преподавателя")
                        .AppendLine("Результат ОК (200): Возвращает дисциплину без преподавателя.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine(
                            "Результат Ошибки (404): Дисциплина или преподаватель не найдены."
                        )
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, NotFound<string>, BadRequest<string>, Ok<SemesterPlanDto>>
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
            new GetEducationPlanQuery(direction.Value, request.Plan.PlanYear),
            ct
        );

        if (educationPlan.IsFailure)
            return TypedResults.NotFound(educationPlan.Error.Description);

        var semester = await queryDispatcher.Dispatch<GetSemesterQuery, Semester>(
            new GetSemesterQuery(educationPlan.Value, request.Semester.Number),
            ct
        );

        if (semester.IsFailure)
            return TypedResults.NotFound(semester.Error.Description);

        var discipline = await queryDispatcher.Dispatch<
            GetDisciplineFromSemesterQuery,
            SemesterPlan
        >(new GetDisciplineFromSemesterQuery(semester.Value, request.Discipline.Discipline), ct);

        if (discipline.IsFailure)
            return TypedResults.NotFound(discipline.Error.Description);

        discipline = await commandDispatcher.Dispatch<
            DeattachTeacherFromDisciplineCommand,
            SemesterPlan
        >(new DeattachTeacherFromDisciplineCommand(discipline.Value), ct);

        return discipline.IsFailure
            ? TypedResults.BadRequest(discipline.Error.Description)
            : TypedResults.Ok(discipline.Value.MapFromDomain());
    }
}
