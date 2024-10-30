using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.EducationDirections.Contracts;
using SPerfomance.Api.Features.EducationPlans.Contracts;
using SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;
using SPerfomance.Application.EducationPlans.Commands.ChangeEducationPlanYear;
using SPerfomance.Application.EducationPlans.DTO;
using SPerfomance.Application.EducationPlans.Queries.GetEducationPlan;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;
using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Models.EducationPlans.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Api.Features.EducationPlans;

public static class ChangeEducationPlanYear
{
	public record Request(
		EducationDirectionContract Direction,
		EducationPlanContract Initial,
		EducationPlanContract Updated,
		TokenContract Token);

	public sealed class Endpoint : IEndpoint
	{
		public void MapEndpoint(IEndpointRouteBuilder app) =>
			app.MapPut($"{EducationPlanTags.Api}", Handler).WithTags(EducationPlanTags.Tag);
	}

	public async static Task<IResult> Handler(
		[FromBody] Request request,
		IUsersRepository users,
		IEducationDirectionRepository directions,
		IEducationPlansRepository plans
	)
	{
		if (!await new UserVerificationService(users).IsVerified(request.Token, UserRole.Administrator))
			return Results.BadRequest(UserTags.UnauthorizedError);

		Result<EducationDirection> direction = await new GetEducationDirectionQueryHandler(directions).Handle(request.Direction);
		Result<EducationPlan> educationPlan = await new GetEducationPlanQueryHandler().Handle(new(
			direction.Value,
			request.Initial.PlanYear
		));

		educationPlan = await new ChangeEducationPlanYearCommandHandler(plans).Handle(new(
			educationPlan.Value,
			request.Updated.PlanYear
		));

		return educationPlan.IsFailure ?
			Results.BadRequest(educationPlan.Error.Description) :
			Results.Ok(educationPlan.Value.MapFromDomain());
	}
}
