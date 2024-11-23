using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Abstractions;

namespace SPerfomance.Api.Features.PerfomanceContext.Features;

public static class GetAssignmentSessionReportsCount
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{PerfomanceContextTags.SessionsApp}/count", Handler)
                .WithTags($"{PerfomanceContextTags.SessionsTag}")
                .WithOpenApi()
                .WithName("GetAssignmentSessionReportsCount")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод возвращает количество отчётов о контрольных неделях")
                        .AppendLine("Результат ОК (200): Количество отчетов о контрольных неделях.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
    }

    public static async Task<Results<UnauthorizedHttpResult, Ok<int>>> Handler(
        [FromHeader(Name = "token")] string token,
        IUsersRepository users,
        IControlWeekReportRepository reports,
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerified(users, ct))
            return TypedResults.Unauthorized();
        return TypedResults.Ok(await reports.Count(ct));
    }
}
