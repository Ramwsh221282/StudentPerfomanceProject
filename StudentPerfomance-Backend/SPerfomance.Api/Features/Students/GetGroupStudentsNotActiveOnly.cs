using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.StudentGroups.Contracts;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Application.StudentGroups.Queries.GetStudentGroupByName;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Models.Students.ValueObjects;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Api.Features.Students;

public class GetGroupStudentsNotActiveOnly
{
	public record Request(StudentGroupContract Group, TokenContract Token);

	public sealed class Endpoint : IEndpoint
	{
		public void MapEndpoint(IEndpointRouteBuilder app) =>
			app.MapPost($"{StudentTags.Api}/by-group-not-active-only", Handler).WithTags($"{StudentTags.Tag}");
	}

	public static async Task<IResult> Handler([FromBody] Request request, IStudentGroupsRepository groups, IUsersRepository users)
	{
		if (!await new UserVerificationService(users).IsVerified(request.Token, UserRole.Administrator))
			return Results.BadRequest(UserTags.UnauthorizedError);

		Result<StudentGroup> group = await new GetStudentGroupQueryHandler(groups).Handle(request.Group);
		if (group.IsFailure)
			return Results.BadRequest(group.Error.Description);

		return Results.Ok(group.Value.Students.Where(s => s.State == StudentState.NotActive).Select(s => s.MapFromDomain()));
	}
}
