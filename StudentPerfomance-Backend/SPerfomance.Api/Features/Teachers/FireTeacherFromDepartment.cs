using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.Teachers.Contracts;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.Departments.Commands.FireTeacher;
using SPerfomance.Application.Departments.DTO;
using SPerfomance.Application.Departments.Queries.GetDepartmentByName;
using SPerfomance.Application.Departments.Queries.GetTeacherFromDepartment;
using SPerfomance.Domain.Models.TeacherDepartments;
using SPerfomance.Domain.Models.Teachers;

namespace SPerfomance.Api.Features.Teachers;

public static class FireTeacherFromDepartment
{
    public record Request(GetDepartmentByNameQuery Department, TeacherContract Teacher);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapDelete($"{TeacherTags.Api}", Handler)
                .WithTags(TeacherTags.Tag)
                .WithOpenApi()
                .WithName("FireTeacherFromDepartment")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод удаляет преподавателя из кафедры")
                        .AppendLine("Результат ОК (200): Удаленный преподаватель в кафедре.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine("Результат Ошибки (404): Преподаватель не найден.")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, NotFound<string>, BadRequest<string>, Ok<TeacherDto>>
    > Handler(
        [FromHeader(Name = "token")] string? token,
        [FromBody] Request request,
        IUsersRepository users,
        IQueryDispatcher queryDispatcher,
        ICommandDispatcher commandDispatcher,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        var jwtToken = new Token(token);
        logger.LogInformation("Запрос на удаление преподавателя из кафедры");
        if (!await jwtToken.IsVerifiedAdmin(users, ct))
        {
            logger.LogError("Пользователь не является администратором");
            return TypedResults.Unauthorized();
        }

        var department = await queryDispatcher.Dispatch<
            GetDepartmentByNameQuery,
            TeachersDepartments
        >(request.Department, ct);

        if (department.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на удаление преподавателя из кафедры отменен. Причина: {text}",
                jwtToken.UserId,
                department.Error.Description
            );
            return TypedResults.NotFound(department.Error.Description);
        }

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
                "Запрос пользователя {id} на удаление преподавателя из кафедры отменен. Причина: {text}",
                jwtToken.UserId,
                teacher.Error.Description
            );
            return TypedResults.NotFound(teacher.Error.Description);
        }

        teacher = await commandDispatcher.Dispatch<FireTeacherCommand, Teacher>(
            new FireTeacherCommand(teacher.Value),
            ct
        );

        if (teacher.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на удаление преподавателя из кафедры отменен. Причина: {text}",
                jwtToken.UserId,
                teacher.Error.Description
            );
            return TypedResults.BadRequest(teacher.Error.Description);
        }

        logger.LogInformation(
            "Пользователь {id} удаляет преподавателя {tname} {tsurname}",
            jwtToken.UserId,
            request.Teacher.Name,
            request.Teacher.Surname
        );
        return TypedResults.Ok(teacher.Value.MapFromDomain());
    }
}
