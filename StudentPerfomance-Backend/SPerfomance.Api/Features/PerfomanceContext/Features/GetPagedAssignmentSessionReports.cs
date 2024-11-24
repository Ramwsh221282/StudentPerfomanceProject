using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.PerfomanceContext.Responses;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Abstractions;
using SPerfomance.Statistics.DataAccess.Repositories;

namespace SPerfomance.Api.Features.PerfomanceContext.Features;

public static class GetPagedAssignmentSessionReports
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{PerfomanceContextTags.SessionsApp}/reports-paged", Handler)
                .WithTags($"{PerfomanceContextTags.SessionsTag}")
                .WithOpenApi()
                .WithName("GetPagedAssignmentSessionReports")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод возвращает отчёты контрольных недель постранично")
                        .AppendLine("Результат ОК (200): Отчёты контрольных недель постранично.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, Ok<string>, Ok<ControlWeekReportDTO[]>>
    > Handler(
        [FromHeader(Name = "token")] string token,
        [FromQuery(Name = "page")] int page,
        [FromQuery(Name = "pageSize")] int pageSize,
        IUsersRepository users,
        IControlWeekReportRepository reports,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation("Запрос на получение отчетов контрольных недель постранично");
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerified(users, ct))
        {
            logger.LogError("Пользователь не авторизован");
            return TypedResults.Unauthorized();
        }

        if (reports is not ControlWeekRepository repository)
        {
            logger.LogError("Репозиторий не ControlWeekRepository");
            return TypedResults.Unauthorized();
        }

        var list = await repository.GetPaged(page, pageSize, ct);
        var result = await ControlWeekReportDTO.InitializeArrayAsync(list);
        logger.LogInformation(
            "Пользователь {id} получает отчёты контрольных недель",
            jwtToken.UserId
        );
        return TypedResults.Ok(result);
    }
}
