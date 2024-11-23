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
        [FromHeader(Name = "token")] string token,
        [FromQuery(Name = "id")] string id,
        IUsersRepository users,
        IControlWeekReportRepository controlWeeks,
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerified(users, ct))
            return TypedResults.Unauthorized();

        if (controlWeeks is not ControlWeekRepository repository)
            return TypedResults.Ok("OK");

        var report = await repository.GetById(Guid.Parse(id), ct);
        return report == null
            ? TypedResults.BadRequest("Отчёт не найден")
            : TypedResults.Ok(new ControlWeekReportDTO(report).GroupParts);
    }
}
