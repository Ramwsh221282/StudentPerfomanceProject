using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.PerfomanceContext.Responses;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Abstractions;
using SPerfomance.Statistics.DataAccess.Repositories;

namespace SPerfomance.Api.Features.PerfomanceContext.Features;

public static class GetCourseReportsByReportId
{
    public sealed record Request(TokenContract Token, Guid Id);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{PerfomanceContextTags.SessionsApp}/course-report-by-id", Handler)
                .WithTags($"{PerfomanceContextTags.SessionsTag}");
    }

    public static async Task<IResult> Handler(
        Request request,
        IUsersRepository usersRepository,
        IControlWeekReportRepository controlWeekReportRepository,
        CancellationToken ct
    )
    {
        if (!await request.Token.IsVerified(usersRepository))
            return Results.BadRequest(
                "Просмотр отчётов доступен только администраторам или преподавателям"
            );

        if (controlWeekReportRepository is not ControlWeekRepository repository)
            return Results.Ok();
        var report = await repository.GetDirectionCodeTypeCourseReportsById(request.Id, ct);
        return report == null
            ? Results.BadRequest("Отчёт не найден")
            : Results.Ok(new ControlWeekReportDTO(report));
    }
}
