using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.StudentGroups.Contracts;
using SPerfomance.Application.StudentGroups.Commands.MergeWithGroup;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Application.StudentGroups.Queries.GetStudentGroupByName;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Api.Features.StudentGroups;

public static class MergeGroups
{
	public record Request(
		StudentGroupContract Initial,
		StudentGroupContract Target,
		TokenContract Token
	);

	public sealed class Endpoint : IEndpoint
	{
		public void MapEndpoint(IEndpointRouteBuilder app) =>
			app.MapPut($"{StudentGroupTags.Api}/merge", Handler).WithTags(StudentGroupTags.Tag);
	}

	public static async Task<IResult> Handler([FromBody] Request request, IUsersRepository users, IStudentGroupsRepository repository)
	{
		if (!await new UserVerificationService(users).IsVerified(request.Token, UserRole.Administrator))
			return Results.BadRequest(UserTags.UnauthorizedError);

		Result<StudentGroup> initial = await new GetStudentGroupQueryHandler(repository).Handle(request.Initial);
		Result<StudentGroup> target = await new GetStudentGroupQueryHandler(repository).Handle(request.Target);

		Result<StudentGroup> result = await new MergeWithGroupCommandHandler(repository)
		.Handle(new(initial.Value, target.Value));
		return result.IsFailure ?
			Results.BadRequest(result.Error.Description) :
			Results.Ok(result.Value.MapFromDomain());
	}
}
