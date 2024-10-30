using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.StudentGroups.Contracts;
using SPerfomance.Application.StudentGroups.Commands.CreateStudentGroup;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Api.Features.StudentGroups;

public static class RegisterStudentGroup
{
	public record Request(StudentGroupContract Group, TokenContract Token);

	public sealed class Endpoint : IEndpoint
	{
		public void MapEndpoint(IEndpointRouteBuilder app) =>
			app.MapPost($"{StudentGroupTags.Api}", Handler).WithTags(StudentGroupTags.Tag);
	}

	public static async Task<IResult> Handler(Request request, IStudentGroupsRepository repository, IUsersRepository users)
	{
		if (!await new UserVerificationService(users).IsVerified(request.Token, UserRole.Administrator))
			return Results.BadRequest(UserTags.UnauthorizedError);

		Result<StudentGroup> group = await new CreateStudentGroupCommandHandler(repository).Handle(new(request.Group.Name));

		return group.IsFailure ?
			Results.BadRequest(group.Error.Description) :
			Results.Ok(group.Value.MapFromDomain());
	}
}
