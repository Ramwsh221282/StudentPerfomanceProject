using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.EducationDirections.Contracts;
using SPerfomance.Api.Features.EducationPlans.Contracts;
using SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;
using SPerfomance.Application.EducationPlans.Commands.ChangeEducationPlanYear;
using SPerfomance.Application.EducationPlans.DTO;
using SPerfomance.Application.EducationPlans.Queries.GetEducationPlan;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;
using SPerfomance.Domain.Models.EducationPlans.Abstractions;

namespace SPerfomance.Api.Features.EducationPlans;

public static class ChangeEducationPlanYear
{
    public record Request(
        EducationDirectionContract Direction,
        EducationPlanContract Initial,
        EducationPlanContract Updated,
        TokenContract Token
    );

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPut($"{EducationPlanTags.Api}", Handler).WithTags(EducationPlanTags.Tag);
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
        var educationPlan = await new GetEducationPlanQueryHandler().Handle(
            new GetEducationPlanQuery(direction.Value, request.Initial.PlanYear),
            ct
        );

        educationPlan = await new ChangeEducationPlanYearCommandHandler(plans).Handle(
            new ChangeEducationPlanYearCommand(educationPlan.Value, request.Updated.PlanYear),
            ct
        );

        return educationPlan.IsFailure
            ? Results.BadRequest(educationPlan.Error.Description)
            : Results.Ok(educationPlan.Value.MapFromDomain());
    }
}
