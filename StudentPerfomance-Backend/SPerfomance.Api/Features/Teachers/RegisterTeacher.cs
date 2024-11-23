using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.Teachers.Contracts;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.Departments.Commands.RegisterTeacher;
using SPerfomance.Application.Departments.DTO;
using SPerfomance.Application.Departments.Queries.GetDepartmentByName;
using SPerfomance.Domain.Models.TeacherDepartments;
using SPerfomance.Domain.Models.Teachers;

namespace SPerfomance.Api.Features.Teachers;

public static class RegisterTeacher
{
    public record Request(GetDepartmentByNameQuery Department, TeacherContract Teacher);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{TeacherTags.Api}", Handler)
                .WithTags(TeacherTags.Tag)
                .WithOpenApi()
                .WithName("CreateTeacher")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод добавляет преподавателя в кафедру")
                        .AppendLine("Результат ОК (200): Созданная кафедра.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine("Результат Ошибки (404): Кафедра не найдена.")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, NotFound<string>, BadRequest<string>, Ok<TeacherDto>>
    > Handler(
        [FromHeader(Name = "token")] string token,
        Request request,
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

        var teacher = await commandDispatcher.Dispatch<RegisterTeacherCommand, Teacher>(
            new RegisterTeacherCommand(
                department.Value,
                request.Teacher.Name,
                request.Teacher.Surname,
                request.Teacher.Patronymic,
                request.Teacher.Job,
                request.Teacher.State
            ),
            ct
        );

        return teacher.IsFailure
            ? TypedResults.BadRequest(teacher.Error.Description)
            : TypedResults.Ok(teacher.Value.MapFromDomain());
    }
}
