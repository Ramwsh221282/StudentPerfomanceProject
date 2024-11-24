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
using SPerfomance.Application.Semesters.Commands.RemoveDiscipline;
using SPerfomance.Application.Semesters.DTO;
using SPerfomance.Application.Semesters.Queries.GetDisciplineFromSemester;
using SPerfomance.Application.Semesters.Queries.GetSemester;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.Semesters;

namespace SPerfomance.Api.Features.SemesterPlans;

public static class RemoveSemesterPlanFromSemester
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
            app.MapDelete($"{SemesterPlanTags.Api}", Handler)
                .WithTags(SemesterPlanTags.Tag)
                .WithOpenApi()
                .WithName("RemoveSemesterPlan")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод удаляет дисциплину из семестра")
                        .AppendLine("Результат ОК (200): Удаленная дисциплина.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine("Результат Ошибки (404): Дисциплина не найдена.")
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
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation("Запрос на удаление дисциплины из семестра");
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
                "Запрос на удаление дисциплины из семестра пользователя {id} отменён. Причина: {text}",
                jwtToken.UserId,
                direction.Error.Description
            );
            return TypedResults.NotFound(direction.Error.Description);
        }

        var educationPlan = await queryDispatcher.Dispatch<GetEducationPlanQuery, EducationPlan>(
            new GetEducationPlanQuery(direction.Value, request.Plan.PlanYear),
            ct
        );

        if (educationPlan.IsFailure)
        {
            logger.LogError(
                "Запрос на удаление дисциплины из семестра пользователя {id} отменён. Причина: {text}",
                jwtToken.UserId,
                educationPlan.Error.Description
            );
            return TypedResults.NotFound(educationPlan.Error.Description);
        }

        var semester = await queryDispatcher.Dispatch<GetSemesterQuery, Semester>(
            new GetSemesterQuery(educationPlan.Value, request.Semester.Number),
            ct
        );

        if (semester.IsFailure)
        {
            logger.LogError(
                "Запрос на удаление дисциплины из семестра пользователя {id} отменён. Причина: {text}",
                jwtToken.UserId,
                semester.Error.Description
            );
            return TypedResults.NotFound(semester.Error.Description);
        }

        var semesterPlan = await queryDispatcher.Dispatch<
            GetDisciplineFromSemesterQuery,
            SemesterPlan
        >(new GetDisciplineFromSemesterQuery(semester.Value, request.Discipline.Discipline), ct);

        if (semesterPlan.IsFailure)
        {
            logger.LogError(
                "Запрос на удаление дисциплины из семестра пользователя {id} отменён. Причина: {text}",
                jwtToken.UserId,
                semesterPlan.Error.Description
            );
            return TypedResults.NotFound(semesterPlan.Error.Description);
        }

        semesterPlan = await commandDispatcher.Dispatch<RemoveDisciplineCommand, SemesterPlan>(
            new RemoveDisciplineCommand(semesterPlan.Value),
            ct
        );

        if (semesterPlan.IsFailure)
        {
            logger.LogError(
                "Запрос на удаление дисциплины из семестра пользователя {id} отменён. Причина: {text}",
                jwtToken.UserId,
                semesterPlan.Error.Description
            );
            return TypedResults.BadRequest(semesterPlan.Error.Description);
        }

        logger.LogInformation(
            "Пользователь {id} удаляет дисциплину {dname} из семестра {semester} учебного плана {planYear} направления подготовки {code} {name} {type}",
            jwtToken.UserId,
            request.Discipline.Discipline,
            request.Semester.Number,
            request.Plan.PlanYear,
            request.Direction.Code,
            request.Direction.Name,
            request.Direction.Type
        );
        return TypedResults.Ok(semesterPlan.Value.MapFromDomain());
    }
}
