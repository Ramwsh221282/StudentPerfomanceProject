using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.Departments.Commands.CreateTeachersDepartment;
using SPerfomance.Application.Departments.DTO;
using SPerfomance.Domain.Models.TeacherDepartments;

namespace SPerfomance.Api.Features.TeacherDepartments;

public static class RegisterTeacherDepartment
{
    public record Request(CreateTeachersDepartmentCommand Department);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{TeacherDepartmentsTags.Api}", Handler)
                .WithTags(TeacherDepartmentsTags.Tag)
                .WithOpenApi()
                .WithName("RegisterTeacherDepartments")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод создаёт кафедру")
                        .AppendLine("Результат ОК (200): Созданная кафедра.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, BadRequest<string>, Ok<TeachersDepartmentDto>>
    > Handler(
        [FromHeader(Name = "token")] string token,
        Request request,
        IUsersRepository users,
        ICommandDispatcher dispatcher,
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerifiedAdmin(users, ct))
            return TypedResults.Unauthorized();
        var department = await dispatcher.Dispatch<
            CreateTeachersDepartmentCommand,
            TeachersDepartments
        >(request.Department, ct);
        return department.IsFailure
            ? TypedResults.BadRequest(department.Error.Description)
            : TypedResults.Ok(department.Value.MapFromDomain());
    }
}
