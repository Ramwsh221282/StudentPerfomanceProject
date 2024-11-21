using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Abstractions;

namespace SPerfomance.Api.Features.PerfomanceContext.Features;

public static class GetAssignmentSessionReportsCount
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{PerfomanceContextTags.SessionsApp}/count", Handler)
                .WithTags($"{PerfomanceContextTags.SessionsTag}");
    }

    public static async Task<IResult> Handler(
        TokenContract token,
        IUsersRepository users,
        IControlWeekReportRepository reports
    )
    {
        if (!await token.IsVerified(users))
            return Results.BadRequest(
                "Просмотр отчётов доступен только администраторам и преподавателям"
            );

        return Results.Ok(await reports.Count());
    }
}
