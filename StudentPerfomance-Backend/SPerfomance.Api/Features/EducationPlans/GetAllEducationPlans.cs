using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Application.EducationPlans.DTO;
using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Models.EducationPlans.Abstractions;

namespace SPerfomance.Api.Features.EducationPlans;

public static class GetAllEducationPlans
{
	public record Request(TokenContract Token);

	public sealed class Endpoint : IEndpoint
	{
		public void MapEndpoint(IEndpointRouteBuilder app) =>
			app.MapPost($"{EducationPlanTags.Api}/all", Handler).WithTags(EducationPlanTags.Tag);
	}

	public static async Task<IResult> Handler(Request request, IUsersRepository users, IEducationPlansRepository repository)
	{
		if (!await new UserVerificationService(users).IsVerified(request.Token, UserRole.Administrator))
			return Results.BadRequest(UserTags.UnauthorizedError);

		IReadOnlyCollection<EducationPlan> plans = await repository.GetAll();
		return Results.Ok(plans.Select(p => p.MapFromDomain()));
	}
}
