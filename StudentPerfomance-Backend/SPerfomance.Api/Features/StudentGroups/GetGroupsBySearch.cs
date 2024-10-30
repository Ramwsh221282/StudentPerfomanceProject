using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.StudentGroups.Contracts;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;

namespace SPerfomance.Api.Features.StudentGroups;

public static class GetGroupsBySearch
{
	public record Request(StudentGroupContract Group, TokenContract Token);

	public sealed class Endpoint : IEndpoint
	{
		public void MapEndpoint(IEndpointRouteBuilder app) =>
			app.MapPost($"{StudentGroupTags.Api}/search", Handler).WithTags(StudentGroupTags.Tag);
	}

	public static async Task<IResult> Handler(Request request, IUsersRepository users, IStudentGroupsRepository repository)
	{
		if (!await new UserVerificationService(users).IsVerified(request.Token, UserRole.Administrator))
			return Results.BadRequest(UserTags.UnauthorizedError);

		IReadOnlyCollection<StudentGroup> groups = await repository.Filter(request.Group.Name);
		return Results.Ok(groups.Select(g => g.MapFromDomain()));
	}
}
