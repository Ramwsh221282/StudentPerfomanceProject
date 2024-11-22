using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.PerfomanceContext.Responses;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Abstractions;
using SPerfomance.Statistics.DataAccess.Repositories;

namespace SPerfomance.Api.Features.PerfomanceContext.Features;

public static class GetAssignmentSessionReportById
{
    public record Request(TokenContract Token, string? Id);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{PerfomanceContextTags.SessionsApp}/group-report-by-id", Handler)
                .WithTags($"{PerfomanceContextTags.SessionsTag}");
    }

    public static async Task<IResult> Handler(
        Request request,
        IUsersRepository users,
        IControlWeekReportRepository controlWeeks,
        CancellationToken ct
    )
    {
        if (!await request.Token.IsVerified(users))
            return Results.BadRequest(
                "Просмотр отчётов доступен только авторизованным пользователям"
            );

        if (string.IsNullOrWhiteSpace(request.Id))
            return Results.BadRequest("Не указан Id отчёта");

        if (controlWeeks is not ControlWeekRepository repository)
            return Results.Ok();

        var report = await repository.GetById(Guid.Parse(request.Id), ct);
        return report == null
            ? Results.BadRequest("Отчёт не найден")
            : Results.Ok(new ControlWeekReportDTO(report).GroupParts);
    }
}
