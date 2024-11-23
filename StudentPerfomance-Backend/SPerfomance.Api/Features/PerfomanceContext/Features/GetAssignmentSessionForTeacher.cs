using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Abstractions;
using SPerfomance.Domain.Models.Teachers.Errors;
using SPerfomance.Domain.Models.Users.Errors;

namespace SPerfomance.Api.Features.PerfomanceContext.Features;

public static class GetAssignmentSessionForTeacher
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{PerfomanceContextTags.SessionsApp}/teacher-assignments-info", Handler)
                .WithTags($"{PerfomanceContextTags.SessionsTag}")
                .WithOpenApi()
                .WithName("GetAssignmentSessionForTeacher")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine(
                            "Метод возвращает информацию о сессии контрольной недели для преподавателя."
                        )
                        .AppendLine(
                            "Результат ОК (200): Возвращает сессию контрольной недели для проставления оценок."
                        )
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine("Результат Ошибки (404): Преподаватель не найден")
                        .ToString()
                );
    }

    public static async Task<
        Results<
            UnauthorizedHttpResult,
            NotFound<string>,
            BadRequest<string>,
            Ok<TeacherAssignmentSession>
        >
    > Handler(
        [FromHeader(Name = "token")] string token,
        IUsersRepository users,
        IAssignmentSessionsRepository sessions,
        CancellationToken ct
    )
    {
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedTeacher(users, ct))
            return TypedResults.Unauthorized();

        var user = await users.GetById(jwtToken.UserId, ct);
        if (user == null)
            return TypedResults.NotFound(UserErrors.NotFound().Description);

        var teacher = await users.GetTeacherByUser(user, ct);
        if (teacher == null)
            return TypedResults.NotFound(TeacherErrors.NotFound().Description);

        var session = await sessions.GetAssignmentSessionForTeacher(teacher, ct);
        return session == null
            ? TypedResults.BadRequest(TeacherErrors.NotFound().Description)
            : TypedResults.Ok(session);
    }
}
