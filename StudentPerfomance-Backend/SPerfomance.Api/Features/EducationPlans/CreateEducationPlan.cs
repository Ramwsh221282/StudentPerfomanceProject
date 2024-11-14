using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.EducationDirections.Contracts;
using SPerfomance.Api.Features.EducationPlans.Contracts;
using SPerfomance.Application.EducationDirections.DTO;
using SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;
using SPerfomance.Application.EducationPlans.Commands.CreateEducationPlan;
using SPerfomance.Application.EducationPlans.DTO;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;
using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Models.EducationPlans.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Api.Features.EducationPlans;

public static class CreateEducationPlan
{
    public record Request(
        EducationDirectionContract Direction,
        EducationPlanContract Plan,
        TokenContract Token
    );

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{EducationPlanTags.Api}", Handler).WithTags(EducationPlanTags.Tag);
    }

    public static async Task<IResult> Handler(
        Request request,
        IUsersRepository users,
        IEducationPlansRepository plans,
        IEducationDirectionRepository directions
    )
    {
        if (
            !await new UserVerificationService(users).IsVerified(
                request.Token,
                UserRole.Administrator
            )
        )
            return Results.BadRequest(UserTags.UnauthorizedError);

        Result<EducationDirection> direction = await new GetEducationDirectionQueryHandler(
            directions
        ).Handle(request.Direction);

        Result<EducationPlan> plan = await new CreateEducationPlanCommandHandler(plans).Handle(
            new(direction.Value, request.Plan.PlanYear)
        );

        return plan.IsFailure
            ? Results.BadRequest(plan.Error.Description)
            : Results.Ok(plan.Value.MapFromDomain());
    }
}
