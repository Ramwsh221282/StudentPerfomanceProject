using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Abstractions;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Commands.CloseAssignmentSession;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services.AssignmentSessionViewServices.Handlers;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services.AssignmentSessionViewServices.Views;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Abstractions;
using SPerfomance.Domain.Tools;

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

        AssignmentSessionViewFactory factory = new AssignmentSessionViewFactory(session.Value);
        AssignmentSessionView view = factory.CreateView();
        Result<AssignmentSessionView> insertion = await reports.Insert(view);
        return insertion.IsFailure ? Results.BadRequest(insertion.Error.Description) : Results.Ok();
    }
}
