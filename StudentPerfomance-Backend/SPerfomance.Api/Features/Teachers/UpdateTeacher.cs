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
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerifiedAdmin(users, ct))
            return TypedResults.Unauthorized();

        var department = await queryDispatcher.Dispatch<
            GetDepartmentByNameQuery,
            TeachersDepartments
        >(request.Department, ct);

        if (department.IsFailure)
            return TypedResults.NotFound(department.Error.Description);

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
            return TypedResults.NotFound(teacher.Error.Description);

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

        return teacher.IsFailure
            ? TypedResults.BadRequest(teacher.Error.Description)
            : TypedResults.Ok(teacher.Value.MapFromDomain());
    }
}
