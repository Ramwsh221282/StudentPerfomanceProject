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
                .WithTags($"{PerfomanceContextTags.SessionsTag}");
    }

    public static async Task<IResult> Handler(
        Request request,
        IUsersRepository users,
        IControlWeekReportRepository reports,
        CancellationToken ct
    )
    {
        if (!await request.Token.IsVerified(users))
            return Results.BadRequest(
                "Просмотр отчётов доступен только администраторам и преподавателям"
            );

        if (reports is not ControlWeekRepository repository)
            return Results.Ok();

        var list = await repository.GetPagedFilteredByPeriod(
            request.Pagination.Page,
            request.Pagination.PageSize,
            ConvertToDate(request.Period.StartPeriod),
            ConvertToDate(request.Period.EndPeriod),
            ct
        );

        return Results.Ok(list.Select(r => new ControlWeekReportDTO(r)));
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
