using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.PerfomanceContext.Responses;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Abstractions;
using SPerfomance.Statistics.DataAccess.Repositories;

namespace SPerfomance.Api.Features.PerfomanceContext.Features;

public static class GetAssignmentSessionReportById
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{PerfomanceContextTags.SessionsApp}/group-report-by-id", Handler)
                .WithTags($"{PerfomanceContextTags.SessionsTag}")
                .WithOpenApi()
                .WithName("GetAssignmentSessionReportById")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод возвращает отчёт контрольной недели по группам")
                        .AppendLine("Результат ОК (200): Отчёт контрольной недели.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine("Результат Ошибки (404): Отчёт не найден.")
                        .ToString()
                );
    }

    public static async Task<
        Results<
            UnauthorizedHttpResult,
            BadRequest<string>,
            Ok<string>,
            Ok<GroupStatisticsReportDTO[]>
        >
    > Handler(
        [FromHeader(Name = "token")] string? token,
        [FromQuery(Name = "id")] string id,
        IUsersRepository users,
        IControlWeekReportRepository controlWeeks,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerified(users, ct))
        {
            logger.LogError("Пользователь не авторизован");
            return TypedResults.Unauthorized();
        }

        if (controlWeeks is not ControlWeekRepository repository)
        {
            logger.LogError("Репозиторий не является ControlWeekRepository");
            return TypedResults.Ok("OK");
        }

        var report = await repository.GetById(Guid.Parse(id), ct);
        if (report == null)
        {
            logger.LogError("Отчёт о контрольной неделе не найден");
            return TypedResults.BadRequest("Отчёт не найден");
        }

        logger.LogInformation(
            "Пользователь {id} получает отчёт о контрольной неделе",
            jwtToken.UserId
        );
        return TypedResults.Ok(new ControlWeekReportDTO(report).GroupParts);
    }
}
