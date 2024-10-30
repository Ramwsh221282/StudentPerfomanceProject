using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.EducationDirections.Contracts;
using SPerfomance.Api.Features.EducationPlans.Contracts;
using SPerfomance.Application.EducationDirections.DTO;
using SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;
using SPerfomance.Application.EducationPlans.Commands.RemoveEducationPlan;
using SPerfomance.Application.EducationPlans.DTO;
using SPerfomance.Application.EducationPlans.Queries.GetEducationPlan;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;
using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Models.EducationPlans.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Api.Features.EducationPlans;

public static class RemoveEducationPlan
{
	public record Request(
		EducationDirectionContract Direction,
		EducationPlanContract Plan,
		TokenContract Token
	);

	public sealed class Endpoint : IEndpoint
	{
		public void MapEndpoint(IEndpointRouteBuilder app) =>
			app.MapDelete($"{EducationPlanTags.Api}", Handler).WithTags(EducationPlanTags.Tag);
	}

	public static async Task<IResult> Handler(
		[FromBody] Request request,
		IUsersRepository users,
		IEducationDirectionRepository directions,
		IEducationPlansRepository plans
	)
	{
		if (!await new UserVerificationService(users).IsVerified(request.Token, UserRole.Administrator))
			return Results.BadRequest(UserTags.UnauthorizedError);

		Result<EducationDirection> direction = await new GetEducationDirectionQueryHandler(directions).Handle(request.Direction);
		Result<EducationPlan> plan = await new GetEducationPlanQueryHandler().Handle(new(direction.Value, request.Plan.PlanYear));
		plan = await new RemoveEducationPlanCommandHandler(plans).Handle(new(plan.Value));


		return plan.IsFailure ?
			Results.BadRequest(plan.Error.Description) :
			Results.Ok(plan.Value.MapFromDomain());
	}
}
