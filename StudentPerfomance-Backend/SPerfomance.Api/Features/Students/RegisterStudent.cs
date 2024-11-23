using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.Students.Contracts;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.StudentGroups.Commands.AddStudentCommand;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Application.StudentGroups.Queries.GetStudentGroupByName;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.Students;

namespace SPerfomance.Api.Features.Students;

public static class RegisterStudent
{
    public record Request(StudentContract Student);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{StudentTags.Api}", Handler)
                .WithTags(StudentTags.Tag)
                .WithOpenApi()
                .WithName("RegisterStudent")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод добавляет студента в группу")
                        .AppendLine("Результат ОК (200): Добавленный студент.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine("Результат Ошибки (404): Студенческая группа не найдена")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, NotFound<string>, BadRequest<string>, Ok<StudentDto>>
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

        var group = await queryDispatcher.Dispatch<GetStudentGroupQuery, StudentGroup>(
            new GetStudentGroupQuery(request.Student.GroupName),
            ct
        );

        if (group.IsFailure)
            return TypedResults.NotFound(group.Error.Description);

        var student = await commandDispatcher.Dispatch<AddStudentCommand, Student>(
            new AddStudentCommand(
                request.Student.Name,
                request.Student.Surname,
                request.Student.Patronymic,
                request.Student.State,
                request.Student.Recordbook,
                group.Value
            ),
            ct
        );

        return student.IsFailure
            ? TypedResults.BadRequest(student.Error.Description)
            : TypedResults.Ok(student.Value.MapFromDomain());
    }
}
