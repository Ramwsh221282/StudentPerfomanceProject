using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Commands.CloseAssignmentSession;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Abstractions;
using SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport;
using SPerfomance.Domain.Tools;
using SPerfomance.Statistics.DataAccess.Repositories;

namespace SPerfomance.Api.Features.PerfomanceContext.Features;

public static class CloseAssignmentSession
{
    public record Request(TokenContract Token, string Id);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{PerfomanceContextTags.SessionsApi}/close-session", Handler)
                .WithTags($"{PerfomanceContextTags.SessionsTag}");
    }

    public static async Task<IResult> Handler(
        IUsersRepository users,
        IAssignmentSessionsRepository repository,
        ControlWeekRepository reports,
        Request request
    )
    {
        if (
            !await new UserVerificationService(users).IsVerified(
                request.Token,
                UserRole.Administrator
            )
        )
            return Results.BadRequest(UserTags.UnauthorizedError);

        CloseAssignmentSessionCommand command = new CloseAssignmentSessionCommand(request.Id);
        CloseAssignmentSessionCommandHandler handler = new CloseAssignmentSessionCommandHandler(
            repository
        );

        Result<AssignmentSession> session = await handler.Handle(command);
        if (session.IsFailure)
            return Results.BadRequest(session.Error.Description);

        Result<ControlWeekReport> report = ControlWeekReport.Create(session);
        if (report.IsFailure)
            return Results.BadRequest(report.Error.Description);

        report = await reports.Insert(report);
        if (report.IsFailure)
            return Results.BadRequest(report.Error.Description);

        return Results.Ok();
    }
}
