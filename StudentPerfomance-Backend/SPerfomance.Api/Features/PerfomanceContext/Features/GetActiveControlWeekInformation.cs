using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Queries.GetInfo;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Abstractions;

namespace SPerfomance.Api.Features.PerfomanceContext.Features;

public static class GetActiveControlWeekInformation
{
    public record Request(TokenContract Token);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{PerfomanceContextTags.SessionsApp}/active-session-info", Handler)
                .WithTags($"{PerfomanceContextTags.SessionsTag}");
    }

    public static async Task<IResult> Handler(
        Request request,
        IUsersRepository users,
        IAssignmentSessionsRepository sessions,
        CancellationToken ct
    )
    {
        var isTeacher = await new UserVerificationService(users).IsVerified(
            request.Token,
            UserRole.Teacher,
            ct
        );
        var isAdmin = await new UserVerificationService(users).IsVerified(
            request.Token,
            UserRole.Administrator,
            ct
        );
        if (!isTeacher && !isAdmin)
            return Results.BadRequest(UserTags.UnauthorizedError);

        var response = await new GetAssignmentSessionInfoQueryHandler(sessions).Handle(
            new GetAssignmentSessionInfoQuery(),
            ct
        );

        return response.IsFailure
            ? Results.BadRequest(response.Error.Description)
            : Results.Ok(response.Value);
    }
}
