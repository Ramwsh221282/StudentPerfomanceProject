using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.PerfomanceContext.Responses;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Abstractions;
using SPerfomance.Statistics.DataAccess.Repositories;

namespace SPerfomance.Api.Features.PerfomanceContext.Features;

public static class GetCourseReportsByReportId
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{PerfomanceContextTags.SessionsApp}/course-report-by-id", Handler)
                .WithTags($"{PerfomanceContextTags.SessionsTag}")
                .WithOpenApi()
                .WithName("GetCourseReportsByReportId")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine(
                            "Метод возвращает отчёты по курсам по ид отчёта контрольной недели"
                        )
                        .AppendLine(
                            "Результат ОК (200): Возвращает сессию контрольной недели для проставления оценок."
                        )
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                )
                .RequireCors("Frontend");
    }

    public static async Task<
        Results<
            UnauthorizedHttpResult,
            Ok<string>,
            NotFound<string>,
            BadRequest<string>,
            Ok<ControlWeekReportDTO>
        >
    > Handler(
        [FromHeader(Name = "token")] string? token,
        [FromQuery(Name = "id")] string id,
        IUsersRepository usersRepository,
        IControlWeekReportRepository controlWeekReportRepository,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation("Запрос на получение отчётов контрольной недели по курсам");
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerified(usersRepository, ct))
        {
            logger.LogError("Пользователь не авторизован");
            return TypedResults.Unauthorized();
        }

        if (controlWeekReportRepository is not ControlWeekRepository repository)
        {
            logger.LogError("Репозиторий не ControlWeekReportRepository");
            return TypedResults.Ok("OK");
        }

        var report = await repository.GetDirectionCodeTypeCourseReportsById(Guid.Parse(id), ct);
        if (report == null)
        {
            logger.LogError("Отчёт контрольной недели о курсах не найден");
            return TypedResults.NotFound("Отчёт не найден");
        }

        logger.LogInformation(
            "Пользователь {id} получает отчёт о курсах контрольной недели",
            jwtToken.UserId
        );
        return TypedResults.Ok(new ControlWeekReportDTO(report));
    }
}
