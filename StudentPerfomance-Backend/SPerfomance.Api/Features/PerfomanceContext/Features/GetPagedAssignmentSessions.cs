using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services.AssignmentSessionViewServices.Handlers;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Abstractions;

namespace SPerfomance.Api.Features.PerfomanceContext.Features;

public static class GetPagedAssignmentSessions
{
    public record Request(PaginationContract Pagination, TokenContract Token);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{PerfomanceContextTags.SessionsApi}/byPage", Handler)
                .WithTags($"{PerfomanceContextTags.SessionsTag}");
    }

    public static async Task<IResult> Handler(
        Request request,
        IUsersRepository users,
        IAssignmentSessionsRepository sessionsRepository,
        CancellationToken ct
    )
    {
        if (!await request.Token.IsVerifiedAdmin(users))
            return Results.BadRequest(UserTags.UnauthorizedError);

        var sessions = await sessionsRepository.GetPaged(
            request.Pagination.Page,
            request.Pagination.PageSize,
            ct
        );

        return Results.Ok(sessions.Select(s => new AssignmentSessionViewFactory(s).CreateView()));
    }
}
