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
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine(
                            "Метод позволяет проставить оценку в контрольной неделе преподавателю"
                        )
                        .AppendLine("Результат ОК (200): Возвращает оценку.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                )
                .RequireCors("Frontend");
    }

    public static async Task<
        Results<UnauthorizedHttpResult, BadRequest<string>, Ok<StudentMarkAssignmentFromTeacherDTO>>
    > Handler(
        Request request,
        IUsersRepository users,
        ICommandDispatcher dispatcher,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation("Запрос на проставление оценки за контрольную неделю");
        var jwtToken = new Token(request.Token.Token);
        if (
            !await jwtToken.IsVerifiedTeacher(users, ct)
            && !await jwtToken.IsVerifiedAdmin(users, ct)
        )
        {
            logger.LogError("Пользователь не является ни преподавателем, ни администратором");
            return TypedResults.Unauthorized();
        }

        var command = new MakeAssignmentCommand(request.Assignment.Id, request.Assignment.Mark);
        var assignment = await dispatcher.Dispatch<MakeAssignmentCommand, StudentAssignment>(
            command,
            ct
        );
        if (assignment.IsFailure)
        {
            logger.LogError(
                "Пользователь {id} не может проставить оценку за контрольную неделю. Причина: {text}",
                jwtToken.UserId,
                assignment.Error.Description
            );
            TypedResults.BadRequest(assignment.Error.Description);
        }

        logger.LogInformation(
            "Пользователь {id} проставляет оценку за контрольную неделю {mark} студенту {name} {surname} {recordbook} {group} по дисциплине {discipline}",
            jwtToken.UserId,
            assignment.Value.Value.Value,
            assignment.Value.Student.Name.Name,
            assignment.Value.Student.Name.Surname,
            assignment.Value.Student.Recordbook.Recordbook,
            assignment.Value.Student.AttachedGroup.Name.Name,
            assignment.Value.Assignment.Discipline.Discipline
        );
        return TypedResults.Ok(new StudentMarkAssignmentFromTeacherDTO(assignment.Value));
    }
}
