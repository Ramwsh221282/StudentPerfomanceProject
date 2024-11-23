using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.TeacherDepartments.Contracts;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.Departments.Commands.ChangeTeachersDepartmentName;
using SPerfomance.Application.Departments.DTO;
using SPerfomance.Application.Departments.Queries.GetDepartmentByName;
using SPerfomance.Domain.Models.TeacherDepartments;

namespace SPerfomance.Api.Features.TeacherDepartments;

public static class ChangeTeacherDepartmentName
{
    public record Request(GetDepartmentByNameQuery Initial, TeacherDepartmentContract Updated);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPut($"{TeacherDepartmentsTags.Api}", Handler)
                .WithTags(TeacherDepartmentsTags.Tag)
                .WithOpenApi()
                .WithName("ChangeDepartmentName")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод изменяет название кафедры")
                        .AppendLine("Результат ОК (200): Кафедра с измененным названием.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine("Результат Ошибки (404): Кафедра не найдена.")
                        .ToString()
                );
    }

    public static async Task<
        Results<
            UnauthorizedHttpResult,
            NotFound<string>,
            BadRequest<string>,
            Ok<TeachersDepartmentDto>
        >
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
        >(request.Initial, ct);

        if (department.IsFailure)
            return TypedResults.NotFound(department.Error.Description);

        department = await commandDispatcher.Dispatch<
            ChangeTeachersDepartmentNameCommand,
            TeachersDepartments
        >(new ChangeTeachersDepartmentNameCommand(department.Value, request.Updated.Name), ct);

        return department.IsFailure
            ? TypedResults.BadRequest(department.Error.Description)
            : TypedResults.Ok(department.Value.MapFromDomain());
    }
}
