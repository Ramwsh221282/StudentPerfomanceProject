using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.EducationDirections.Contracts;
using SPerfomance.Api.Features.EducationPlans.Contracts;
using SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;
using SPerfomance.Application.EducationPlans.Commands.RemoveEducationPlan;
using SPerfomance.Application.EducationPlans.DTO;
using SPerfomance.Application.EducationPlans.Queries.GetEducationPlan;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;
using SPerfomance.Domain.Models.EducationPlans.Abstractions;

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
        IEducationPlansRepository plans,
        CancellationToken ct
    )
    {
        if (
            !await new UserVerificationService(users).IsVerified(
                request.Token,
                UserRole.Administrator,
                ct
            )
        )
            return Results.BadRequest(UserTags.UnauthorizedError);

        var direction = await new GetEducationDirectionQueryHandler(directions).Handle(
            request.Direction,
            ct
        );
        var plan = await new GetEducationPlanQueryHandler().Handle(
            new GetEducationPlanQuery(direction.Value, request.Plan.PlanYear),
            ct
        );
        plan = await new RemoveEducationPlanCommandHandler(plans).Handle(
            new RemoveEducationPlanCommand(plan.Value),
            ct
        );

        return plan.IsFailure
            ? Results.BadRequest(plan.Error.Description)
            : Results.Ok(plan.Value.MapFromDomain());
    }
}
