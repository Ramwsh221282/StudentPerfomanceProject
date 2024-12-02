using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.Departments.DTO;
using SPerfomance.Application.Departments.Queries.GetDepartmentByName;
using SPerfomance.Domain.Models.TeacherDepartments;

namespace SPerfomance.Api.Features.Teachers;

public static class GetTeachersByDepartment
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{TeacherTags.Api}", Handler)
                .WithTags(TeacherTags.Tag)
                .WithOpenApi()
                .WithName("GetTeachersByDepartment")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Возвращает преподавателей кафедры")
                        .AppendLine("Результат ОК (200): Список преподавателей кафедры.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, NotFound<string>, Ok<TeacherDto[]>>
    > Handler(
        [FromHeader(Name = "token")] string? token,
        [FromQuery(Name = "departmentName")] string departmentName,
        IUsersRepository users,
        IQueryDispatcher dispatcher,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation("Запрос на получение преподавателей из кафедры");
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedAdmin(users, ct))
        {
            logger.LogError("Пользователь не является администратором");
            return TypedResults.Unauthorized();
        }

        var query = new GetDepartmentByNameQuery(departmentName);
        var department = await dispatcher.Dispatch<GetDepartmentByNameQuery, TeachersDepartments>(
            query,
            ct
        );

        if (department.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на получение преподавателей из кафедры отменён. Причина: {text}",
                jwtToken.UserId,
                department.Error.Description
            );
            return TypedResults.NotFound(department.Error.Description);
        }

        TeacherDto[] teachers = department
            .Value.Teachers.Select(t => t.MapFromDomain())
            .OrderBy(t => t.Surname)
            .ToArray();
        logger.LogInformation(
            "Пользователь {id} получает преподавателей кафедры {dname} {count}",
            jwtToken.UserId,
            departmentName,
            teachers.Length
        );
        return TypedResults.Ok(teachers);
    }
}
