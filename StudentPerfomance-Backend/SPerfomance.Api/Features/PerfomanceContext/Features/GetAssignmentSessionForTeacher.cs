using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.PerfomanceContext.Features.GetAssignmentSessionForUser;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Abstractions;
using SPerfomance.Domain.Models.Teachers.Abstractions;

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
                .RequireRateLimiting("fixed")
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
                )
                .RequireCors("Frontend");
    }

    public static async Task<
        Results<
            UnauthorizedHttpResult,
            NotFound<string>,
            BadRequest<string>,
            Ok<TeacherAssignmentSession>
        >
    > Handler(
        [FromHeader(Name = "token")] string? token,
        [FromHeader(Name = "adminAssignmentsAccess")] string? adminAccess,
        IUsersRepository users,
        IAssignmentSessionsRepository sessions,
        ITeachersRepository teachers,
        CancellationToken ct
    )
    {
        if (!string.IsNullOrWhiteSpace(adminAccess))
            return await new AdminAccessStrategy(
                users,
                sessions,
                teachers,
                adminAccess,
                ct
            ).GetAssignmentSessions();
        if (!string.IsNullOrWhiteSpace(token))
            return await new TeacherAccessStrategy(
                token,
                users,
                sessions,
                ct
            ).GetAssignmentSessions();
        return TypedResults.BadRequest("Невозможно разрешить доступ");
    }
}
