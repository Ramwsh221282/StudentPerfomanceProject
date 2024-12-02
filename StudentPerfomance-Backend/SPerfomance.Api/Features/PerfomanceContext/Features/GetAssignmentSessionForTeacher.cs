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
        [FromHeader(Name = "token")] string? token,
        IUsersRepository users,
        IAssignmentSessionsRepository sessions,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation("Запрос на получение контрольных недель для преподавателя");
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedTeacher(users, ct))
        {
            logger.LogError("Пользователь не является преподавателем");
            return TypedResults.Unauthorized();
        }

        var user = await users.GetById(jwtToken.UserId, ct);
        if (user == null)
        {
            logger.LogError("Пользователь не найден");
            return TypedResults.NotFound(UserErrors.NotFound().Description);
        }

        var teacher = await users.GetTeacherByUser(user, ct);
        if (teacher == null)
        {
            logger.LogError(
                "Пользователь {name} {surname} {email} {id} не является преподавателем",
                user.Name.Name,
                user.Name.Surname,
                user.Email.Email,
                user.Id
            );
            return TypedResults.NotFound(TeacherErrors.NotFound().Description);
        }

        var session = await sessions.GetAssignmentSessionForTeacher(teacher, ct);
        if (session == null)
        {
            logger.LogError(
                "Нет активной контрольной недели для преподавателя {name} {surname} {id}",
                teacher.Name.Name,
                teacher.Name.Surname,
                teacher.Id
            );
            return TypedResults.BadRequest(TeacherErrors.NotFound().Description);
        }

        logger.LogInformation(
            "Преподаватель {name} {surname} {id} получает контрольную неделю",
            teacher.Name.Name,
            teacher.Name.Surname,
            teacher.Id
        );
        return TypedResults.Ok(session);
    }
}
