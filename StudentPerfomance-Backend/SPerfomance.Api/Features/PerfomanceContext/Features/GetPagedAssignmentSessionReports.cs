using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.PerfomanceContext.Responses;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Abstractions;
using SPerfomance.Statistics.DataAccess.EntityModels;
using SPerfomance.Statistics.DataAccess.Repositories;

namespace SPerfomance.Api.Features.PerfomanceContext.Features;

public static class GetPagedAssignmentSessionReports
{
    public sealed record Request(TokenContract Token, PaginationContract Pagination);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{PerfomanceContextTags.SessionsApp}/reports-paged", Handler)
                .WithTags($"{PerfomanceContextTags.SessionsTag}");
    }

    public static async Task<IResult> Handler(
        Request request,
        IUsersRepository users,
        IControlWeekReportRepository reports
    )
    {
        if (!await request.Token.IsVerified(users))
            return Results.BadRequest(
                "Просмотр отчётов доступен только администраторам или преподавателям"
            );

        if (reports is not ControlWeekRepository repository)
            return Results.NotFound();

        IReadOnlyList<ControlWeekReportEntity> list = await repository.GetPaged(
            request.Pagination.Page,
            request.Pagination.PageSize
        );

        ControlWeekReportDTO[] result = await ControlWeekReportDTO.InitializeArrayAsync(list);
        return Results.Ok(result);
    }
}
