using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.EducationDirections.Contracts;
using SPerfomance.Api.Features.EducationPlans.Contracts;
using SPerfomance.Api.Features.Semesters.Contracts;
using SPerfomance.Api.Features.StudentGroups.Contracts;
using SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;
using SPerfomance.Application.EducationPlans.Queries.GetEducationPlan;
using SPerfomance.Application.StudentGroups.Commands.AttachEducationPlan;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Application.StudentGroups.Queries.GetStudentGroupByName;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;
using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Api.Features.StudentGroups;

public static class AttachGroupEducationPlan
{
	public record Request(
		EducationDirectionContract Direction,
		EducationPlanContract Plan,
		SemesterContract Semester,
		StudentGroupContract Group,
		TokenContract Token
		);

	public sealed class Endpoint : IEndpoint
	{
		public void MapEndpoint(IEndpointRouteBuilder app) =>
			app.MapPut($"{StudentGroupTags.Api}/attach-education-plan", Handler).WithTags($"{StudentGroupTags.Tag}");
	}

	public static async Task<IResult> Handler(
		[FromBody] Request request,
		IUsersRepository users,
		IEducationDirectionRepository directions,
		IStudentGroupsRepository groups
	)
	{
		if (!await new UserVerificationService(users).IsVerified(request.Token, UserRole.Administrator))
			return Results.BadRequest(UserTags.UnauthorizedError);

		Result<EducationDirection> direction = await new GetEducationDirectionQueryHandler(directions).Handle(request.Direction);
		Result<EducationPlan> plan = await new GetEducationPlanQueryHandler()
		.Handle(new(direction.Value, request.Plan.PlanYear));
		Result<StudentGroup> group = await new GetStudentGroupQueryHandler(groups).Handle(request.Group);
		group = await new AttachEducationPlanCommandHandler(groups).Handle(
			new(
				plan.Value,
				group.Value,
				request.Semester.Number
				));

		return group.IsFailure ?
			Results.BadRequest(group.Error.Description) :
			Results.Ok(group.Value.MapFromDomain());
	}
}
