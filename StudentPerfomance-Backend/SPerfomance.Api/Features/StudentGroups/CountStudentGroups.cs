using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;

namespace SPerfomance.Api.Features.StudentGroups;

public static class CountStudentGroups
{
	public record Request(TokenContract Token);

	public sealed class Endpoint : IEndpoint
	{
		public void MapEndpoint(IEndpointRouteBuilder app) =>
			app.MapPost($"{StudentGroupTags.Api}/count", Handler).WithTags(StudentGroupTags.Tag);
	}

	public async static Task<IResult> Handler(Request request, IUsersRepository users, IStudentGroupsRepository repository)
	{
		if (!await new UserVerificationService(users).IsVerified(request.Token, UserRole.Administrator))
			return Results.BadRequest(UserTags.UnauthorizedError);

		int count = await repository.Count();
		return Results.Ok(count);
	}
}
