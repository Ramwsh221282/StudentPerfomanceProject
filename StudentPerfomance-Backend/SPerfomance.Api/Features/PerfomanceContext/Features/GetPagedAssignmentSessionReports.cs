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
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerified(users, ct))
            return TypedResults.Unauthorized();

        if (reports is not ControlWeekRepository repository)
            return TypedResults.Ok("OK");

        var list = await repository.GetPaged(page, pageSize, ct);
        var result = await ControlWeekReportDTO.InitializeArrayAsync(list);
        return TypedResults.Ok(result);
    }
}
