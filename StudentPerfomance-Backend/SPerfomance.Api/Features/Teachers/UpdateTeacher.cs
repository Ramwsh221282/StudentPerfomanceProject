using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.Teachers.Contracts;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.Departments.Commands.UpdateTeacher;
using SPerfomance.Application.Departments.DTO;
using SPerfomance.Application.Departments.Queries.GetDepartmentByName;
using SPerfomance.Application.Departments.Queries.GetTeacherFromDepartment;
using SPerfomance.Domain.Models.TeacherDepartments;
using SPerfomance.Domain.Models.Teachers;

namespace SPerfomance.Api.Features.Teachers;

public static class UpdateTeacher
{
    public record Request(
        GetDepartmentByNameQuery Department,
        TeacherContract Initial,
        TeacherContract Updated
    );

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPut($"{TeacherTags.Api}", Handler)
                .WithTags(TeacherTags.Tag)
                .WithOpenApi()
                .WithName("UpdateTeacher")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод изменяет данные о преподавателе")
                        .AppendLine("Результат ОК (200): Измененные данные преподавателя.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine("Результат Ошибки (404): Преподаватель не найден")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, NotFound<string>, BadRequest<string>, Ok<TeacherDto>>
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
        logger.LogInformation("Запрос на обновление данных о преподавателе");
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedAdmin(users, ct))
        {
            logger.LogInformation("Пользователь не является администратором");
            return TypedResults.Unauthorized();
        }

        var department = await queryDispatcher.Dispatch<
            GetDepartmentByNameQuery,
            TeachersDepartments
        >(request.Department, ct);

        if (department.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на обновление данных о преподавателе отменен. Причина {text}",
                jwtToken.UserId,
                department.Error.Description
            );
            return TypedResults.NotFound(department.Error.Description);
        }

        var teacher = await queryDispatcher.Dispatch<GetTeacherFromDepartmentQuery, Teacher>(
            new GetTeacherFromDepartmentQuery(
                department.Value,
                request.Initial.Name,
                request.Initial.Surname,
                request.Initial.Patronymic,
                request.Initial.Job,
                request.Initial.State
            ),
            ct
        );

        if (teacher.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на обновление данных о преподавателе отменен. Причина {text}",
                jwtToken.UserId,
                teacher.Error.Description
            );
            return TypedResults.NotFound(teacher.Error.Description);
        }

        teacher = await commandDispatcher.Dispatch<UpdateTeacherCommand, Teacher>(
            new UpdateTeacherCommand(
                teacher.Value,
                request.Updated.Name,
                request.Updated.Surname,
                request.Updated.Patronymic,
                request.Updated.Job,
                request.Updated.State
            ),
            ct
        );

        if (teacher.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на обновление данных о преподавателе отменен. Причина {text}",
                jwtToken.UserId,
                teacher.Error.Description
            );
            TypedResults.BadRequest(teacher.Error.Description);
        }

        logger.LogInformation(
            "Пользователь {id} изменяет данные преподавателя {tid}",
            jwtToken.UserId,
            teacher.Value.Id
        );
        return TypedResults.Ok(teacher.Value.MapFromDomain());
    }
}
