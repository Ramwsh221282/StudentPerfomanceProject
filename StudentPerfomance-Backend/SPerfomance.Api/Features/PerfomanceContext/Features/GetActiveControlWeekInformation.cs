using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.DTO;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Queries.GetInfo;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Api.Features.PerfomanceContext.Features;

public static class GetActiveControlWeekInformation
{
	public record Request(TokenContract Token);

	public sealed class Endpoint : IEndpoint
	{
		public void MapEndpoint(IEndpointRouteBuilder app) =>
			app.MapPost($"{PerfomanceContextTags.SessionsApp}/active-session-info", Handler).WithTags($"{PerfomanceContextTags.SessionsTag}");
	}

	public static async Task<IResult> Handler(Request request, IUsersRepository users, IAssignmentSessionsRepository sessions)
	{
		bool isTeacher = await new UserVerificationService(users).IsVerified(request.Token, UserRole.Teacher);
		bool isAdmin = await new UserVerificationService(users).IsVerified(request.Token, UserRole.Administrator);
		if (!isTeacher && !isAdmin)
			return Results.BadRequest(UserTags.UnauthorizedError);

		Result<AssignmentSessionInfoDTO> response = await new GetAssignmentSessionInfoQueryHandler(sessions)
		.Handle(new());

		return response.IsFailure ?
			Results.BadRequest(response.Error.Description) :
			Results.Ok(response.Value);
	}
}
