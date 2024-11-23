using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.Students.Contracts;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.StudentGroups.Commands.RemoveStudent;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Application.StudentGroups.Queries.GetStudentFromGroup;
using SPerfomance.Application.StudentGroups.Queries.GetStudentGroupByName;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.Students;

namespace SPerfomance.Api.Features.Students;

public class FireStudent
{
    public record Request(StudentContract Student);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapDelete($"{StudentTags.Api}", Handler)
                .WithTags(StudentTags.Tag)
                .WithOpenApi()
                .WithName("FireStudent")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод удаляет студента из группы")
                        .AppendLine("Результат ОК (200): Возвращает удаленного студента из группы.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine("Результат Ошибки (404): Студент не найден")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, NotFound<string>, BadRequest<string>, Ok<StudentDto>>
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

        var groupQuery = new GetStudentGroupQuery(request.Student.GroupName);
        var group = await queryDispatcher.Dispatch<GetStudentGroupQuery, StudentGroup>(
            groupQuery,
            ct
        );

        if (group.IsFailure)
            return TypedResults.NotFound(group.Error.Description);

        var student = await queryDispatcher.Dispatch<GetStudentFromGroupQuery, Student>(
            new GetStudentFromGroupQuery(
                group.Value,
                request.Student.Name,
                request.Student.Surname,
                request.Student.Patronymic,
                request.Student.State,
                request.Student.Recordbook
            ),
            ct
        );

        if (student.IsFailure)
            return TypedResults.NotFound(student.Error.Description);

        student = await commandDispatcher.Dispatch<RemoveStudentCommand, Student>(
            new RemoveStudentCommand(student.Value),
            ct
        );

        return student.IsFailure
            ? TypedResults.BadRequest(student.Error.Description)
            : TypedResults.Ok(student.Value.MapFromDomain());
    }
}
