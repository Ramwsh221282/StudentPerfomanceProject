using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.EducationPlans.Contracts;
using SPerfomance.Api.Features.SemesterPlans.Contracts;
using SPerfomance.Api.Features.Semesters.Contracts;
using SPerfomance.Api.Features.Teachers.Contracts;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.Departments.Queries.GetDepartmentByName;
using SPerfomance.Application.Departments.Queries.GetTeacherFromDepartment;
using SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;
using SPerfomance.Application.EducationPlans.Queries.GetEducationPlan;
using SPerfomance.Application.Semesters.Commands.AttachTeacherToDiscipline;
using SPerfomance.Application.Semesters.DTO;
using SPerfomance.Application.Semesters.Queries.GetDisciplineFromSemester;
using SPerfomance.Application.Semesters.Queries.GetSemester;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.Semesters;
using SPerfomance.Domain.Models.TeacherDepartments;
using SPerfomance.Domain.Models.Teachers;

namespace SPerfomance.Api.Features.SemesterPlans;

public static class AttachTeacher
{
    public record Request(
        GetEducationDirectionQuery Direction,
        EducationPlanContract Plan,
        SemesterContract Semester,
        SemesterPlanContract Discipline,
        GetDepartmentByNameQuery Department,
        TeacherContract Teacher
    );

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPut($"{SemesterPlanTags.Api}/attach-teacher", Handler)
                .WithTags(SemesterPlanTags.Tag)
                .WithOpenApi()
                .WithName("AttachTeacher")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод устанавливает преподавателю дисциплину")
                        .AppendLine("Результат ОК (200): Установленная дисциплина.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine(
                            "Результат Ошибки (404): Не найден преподаватель или дисциплина"
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
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation("Запрос на закрепление дисциплины преподавателю");
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedAdmin(users, ct))
        {
            logger.LogError("Пользователь не является администратором");
            return TypedResults.Unauthorized();
        }

        logger.LogInformation(
            "Пользователь {id} запрашивает кафедру для получения преподавателя",
            jwtToken.UserId
        );
        var department = await queryDispatcher.Dispatch<
            GetDepartmentByNameQuery,
            TeachersDepartments
        >(request.Department, ct);

        if (department.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на закрепление дисциплины преподавателю отменён. Причина: {text}",
                jwtToken.UserId,
                department.Error.Description
            );
            return TypedResults.NotFound(department.Error.Description);
        }

        logger.LogInformation(
            "Пользователь {id} запрашивает преподавателя из кафедры для закрепления дисциплины",
            jwtToken.UserId
        );
        var teacher = await queryDispatcher.Dispatch<GetTeacherFromDepartmentQuery, Teacher>(
            new GetTeacherFromDepartmentQuery(
                department.Value,
                request.Teacher.Name,
                request.Teacher.Surname,
                request.Teacher.Patronymic,
                request.Teacher.Job,
                request.Teacher.State
            ),
            ct
        );

        if (teacher.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на закрепление дисциплины преподавателю отменён. Причина: {text}",
                jwtToken.UserId,
                teacher.Error.Description
            );
            return TypedResults.NotFound(teacher.Error.Description);
        }

        logger.LogInformation(
            "Пользователь {id} запрашивает направление подготовки, чтобы получить дисциплину для закрепления преподавателю",
            jwtToken.UserId
        );
        var direction = await queryDispatcher.Dispatch<
            GetEducationDirectionQuery,
            EducationDirection
        >(request.Direction, ct);

        if (direction.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на закрепление дисциплины преподавателю отменён. Причина: {text}",
                jwtToken.UserId,
                direction.Error.Description
            );
            return TypedResults.NotFound(direction.Error.Description);
        }

        logger.LogInformation(
            "Пользователь {id} запрашивает учебный план из направления подготовки, чтобы получить дисциплину для закрепления преподавателю",
            jwtToken.UserId
        );
        var educationPlan = await queryDispatcher.Dispatch<GetEducationPlanQuery, EducationPlan>(
            new GetEducationPlanQuery(direction.Value, request.Plan.PlanYear),
            ct
        );

        if (educationPlan.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на закрепление дисциплины преподавателю отменён. Причина: {text}",
                jwtToken.UserId,
                educationPlan.Error.Description
            );
            return TypedResults.NotFound(educationPlan.Error.Description);
        }

        logger.LogInformation(
            "Пользователь {id} запрашивает семестр из учебного плана, чтобы получить дисциплину для закрепления преподавателю",
            jwtToken.UserId
        );
        var semester = await queryDispatcher.Dispatch<GetSemesterQuery, Semester>(
            new GetSemesterQuery(educationPlan.Value, request.Semester.Number),
            ct
        );

        if (semester.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на закрепление дисциплины преподавателю отменён. Причина: {text}",
                jwtToken.UserId,
                semester.Error.Description
            );
            return TypedResults.NotFound(semester.Error.Description);
        }

        logger.LogInformation(
            "Пользователь {id} запрашивает дисциплину из семестра для закрепления преподавателю",
            jwtToken.UserId
        );
        var discipline = await queryDispatcher.Dispatch<
            GetDisciplineFromSemesterQuery,
            SemesterPlan
        >(new GetDisciplineFromSemesterQuery(semester.Value, request.Discipline.Discipline), ct);

        if (discipline.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на закрепление дисциплины преподавателю отменён. Причина: {text}",
                jwtToken.UserId,
                discipline.Error.Description
            );
            return TypedResults.NotFound(discipline.Error.Description);
        }

        discipline = await commandDispatcher.Dispatch<
            AttachTeacherToDisciplineCommand,
            SemesterPlan
        >(new AttachTeacherToDisciplineCommand(teacher.Value, discipline.Value), ct);

        if (discipline.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на закрепление дисциплины преподавателю отменён. Причина: {text}",
                jwtToken.UserId,
                discipline.Error.Description
            );
            return TypedResults.BadRequest(discipline.Error.Description);
        }

        logger.LogInformation(
            "Пользователь {id} закрепляет дисциплину {name} преподавателю {tname} {tsurname}",
            jwtToken.UserId,
            discipline.Value.Discipline.Name,
            discipline.Value.Teacher!.Name.Name,
            discipline.Value.Teacher!.Name.Surname
        );
        return TypedResults.Ok(discipline.Value.MapFromDomain());
    }
}
