using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Abstractions;

namespace SPerfomance.Api.Features.PerfomanceContext.Features;

public static class GetAssignmentSessionReport
{
    public record Request(TokenContract Token, Guid Id);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{PerfomanceContextTags.SessionsApi}/report", Handler)
                .WithTags($"{PerfomanceContextTags.SessionsTag}");
    }

    public static async Task<IResult> Handler(
        Request request,
        IUsersRepository users,
        IControlWeekReportRepository repository
    )
    {
        if (
            !await new UserVerificationService(users).IsVerified(
                request.Token,
                UserRole.Administrator
            )
        )
            return Results.BadRequest(UserTags.UnauthorizedError);

        return Results.Ok();
    }
}
