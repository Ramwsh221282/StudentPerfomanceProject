using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Models.Teachers.Abstractions;

namespace SPerfomance.Api.Features.PerfomanceContext.Features;

public static class GetAdminAssignmentsAccessEndpoint
{
    public sealed record AdminAssignmentAccessResponse(string AdminId, string TeacherId);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{PerfomanceContextTags.SessionsApp}/admin-assignments-access", Handler)
                .WithTags($"{PerfomanceContextTags.SessionsTag}")
                .WithOpenApi()
                .WithName("GetAssignmentSessionForAdmin")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine(
                            "Метод возвращает идентификаторы доступа администратору для проставления оценок."
                        )
                        .AppendLine(
                            "Результат ОК (200): Возвращает идентификаторы доступа администратору для проставления оценок."
                        )
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine(
                            "Результат Ошибки (404): Преподаватель не найден или пользователь не найден"
                        )
                        .ToString()
                )
                .RequireCors("Frontend");
        }
    }

    public static async Task<
        Results<
            UnauthorizedHttpResult,
            NotFound<string>,
            BadRequest<string>,
            Ok<AdminAssignmentAccessResponse>
        >
    > Handler(
        [FromHeader(Name = "token")] string? token,
        [FromQuery] string? teacherId,
        IUsersRepository users,
        ITeachersRepository teachers,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation(
            "Запрос на получение доступа к проставлению в качестве администратора"
        );
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedAdmin(users))
        {
            logger.LogError("Пользователь не является администратором");
            return TypedResults.Unauthorized();
        }
        if (string.IsNullOrWhiteSpace(teacherId))
        {
            string message = "Идентификатор запрашиваемого преподавателя не указан";
            logger.LogError(message);
            return TypedResults.BadRequest(message);
        }
        Teacher? teacher = await teachers.GetById(teacherId, ct);
        if (teacher == null)
        {
            string message = "Преподаватель не найден";
            logger.LogError(message);
            return TypedResults.BadRequest(message);
        }
        logger.LogInformation("Администратор получил доступ к проставлению оценок");
        return TypedResults.Ok(
            new AdminAssignmentAccessResponse(jwtToken.UserId, teacher.Id.ToString())
        );
    }
}
