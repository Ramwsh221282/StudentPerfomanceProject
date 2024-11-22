using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Abstractions;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Commands.CloseAssignmentSession;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services.AssignmentSessionViewServices.Handlers;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Abstractions;

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
        IControlWeekReportRepository reports,
        Request request,
        CancellationToken ct
    )
    {
        if (
            !await new UserVerificationService(users).IsVerified(
                request.Token,
                UserRole.Administrator,
                ct
            )
        )
            return Results.BadRequest(UserTags.UnauthorizedError);

        var command = new CloseAssignmentSessionCommand(request.Id);
        var handler = new CloseAssignmentSessionCommandHandler(repository);

        var session = await handler.Handle(command, ct);
        if (session.IsFailure)
            return Results.BadRequest(session.Error.Description);

        var factory = new AssignmentSessionViewFactory(session.Value);
        var view = factory.CreateView();
        var insertion = await reports.Insert(view, ct);
        return insertion.IsFailure ? Results.BadRequest(insertion.Error.Description) : Results.Ok();
    }
}
