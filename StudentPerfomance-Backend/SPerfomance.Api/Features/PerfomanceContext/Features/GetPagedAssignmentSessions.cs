using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.DTO;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Abstractions;

namespace SPerfomance.Api.Features.PerfomanceContext.Features;

public static class GetPagedAssignmentSessions
{
	public record Request(PaginationContract Pagination, TokenContract Token);

	public sealed class Endpoint : IEndpoint
	{
		public void MapEndpoint(IEndpointRouteBuilder app) =>
			app.MapPost($"{PerfomanceContextTags.SessionsApi}/byPage", Handler).WithTags($"{PerfomanceContextTags.SessionsTag}");
	}

	public static async Task<IResult> Handler(Request request, IUsersRepository users, IAssignmentSessionsRepository sessionsRepository)
	{
		if (!await new UserVerificationService(users).IsVerified(request.Token, UserRole.Administrator))
			return Results.BadRequest(UserTags.UnauthorizedError);

		IReadOnlyCollection<AssignmentSession> sessions = await sessionsRepository.GetPaged(request.Pagination.Page, request.Pagination.PageSize);
		return Results.Ok(sessions.Select(s => new AssignmentSessionDTO(s)));
	}
}
