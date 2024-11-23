using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.PerfomanceContext.Contracts;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Commands.MakeAssignment;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.DTO;
using SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;

namespace SPerfomance.Api.Features.PerfomanceContext.Features;

public static class MakeAssignmentByTeacher
{
    public record Request(TokenContract Token, TeacherAssignmentContract Assignment);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{PerfomanceContextTags.SessionsApp}/make-assignment", Handler)
                .WithTags($"{PerfomanceContextTags.SessionsTag}")
                .WithOpenApi()
                .WithName("MakeAssignmentByTeacher")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine(
                            "Метод позволяет проставить оценку в контрольной неделе преподавателю"
                        )
                        .AppendLine("Результат ОК (200): Возвращает оценку.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, BadRequest<string>, Ok<StudentMarkAssignmentFromTeacherDTO>>
    > Handler(
        Request request,
        IUsersRepository users,
        ICommandDispatcher dispatcher,
        CancellationToken ct
    )
    {
        if (!await new Token(request.Token.Token).IsVerifiedTeacher(users, ct))
            return TypedResults.Unauthorized();

        var command = new MakeAssignmentCommand(request.Assignment.Id, request.Assignment.Mark);
        var assignment = await dispatcher.Dispatch<MakeAssignmentCommand, StudentAssignment>(
            command,
            ct
        );
        return assignment.IsFailure
            ? TypedResults.BadRequest(assignment.Error.Description)
            : TypedResults.Ok(new StudentMarkAssignmentFromTeacherDTO(assignment.Value));
    }
}
