using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.PerfomanceContext.Contracts;
using SPerfomance.Api.Features.PerfomanceContext.Responses;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Abstractions;
using SPerfomance.Statistics.DataAccess.Repositories;

namespace SPerfomance.Api.Features.PerfomanceContext.Features;

public static class GetPagedFilteredReports
{
    public record Request(
        TokenContract Token,
        PaginationContract Pagination,
        ReportPeriodFilterContract Period
    );

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{PerfomanceContextTags.SessionsApp}/paged-filtered", Handler)
                .WithTags($"{PerfomanceContextTags.SessionsTag}")
                .WithOpenApi()
                .WithName("GetPagedFilteredReports")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine(
                            "Метод возвращает отфильтрованные отчёты контрольных недель постранично"
                        )
                        .AppendLine("Результат ОК (200): Постраничные отчёты контрольных недель.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, Ok<string>, Ok<IEnumerable<ControlWeekReportDTO>>>
    > Handler(
        Request request,
        IUsersRepository users,
        IControlWeekReportRepository reports,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation(
            "Запрос на получение отфильтрованных отчётов о контрольных неделях постранично"
        );
        var jwtToken = new Token(request.Token.Token);
        if (!await jwtToken.IsVerified(users, ct))
        {
            logger.LogError("Пользователь не авторизован");
            return TypedResults.Unauthorized();
        }

        if (reports is not ControlWeekRepository repository)
        {
            logger.LogError("Репозиторий не ControlWeekRepository");
            return TypedResults.Ok("OK");
        }

        var list = await repository.GetPagedFilteredByPeriod(
            request.Pagination.Page,
            request.Pagination.PageSize,
            ConvertToDate(request.Period.StartPeriod),
            ConvertToDate(request.Period.EndPeriod),
            ct
        );

        logger.LogInformation(
            "Пользователь {id} получает отфильтрованные отчёты о контрольных неделях постранично {count}",
            jwtToken.UserId,
            list.Count
        );
        return TypedResults.Ok(list.Select(r => new ControlWeekReportDTO(r)));
    }

    private static DateTime? ConvertToDate(DatePeriod? period)
    {
        if (period?.Day == null || period.Month == null || period.Year == null)
            return null;

        try
        {
            return new DateTime(period.Year.Value, period.Month.Value, period.Day.Value);
        }
        catch
        {
            return null;
        }
    }
}
