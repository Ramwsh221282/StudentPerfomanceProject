using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.EducationDirections.Contracts;
using SPerfomance.Api.Features.EducationPlans.Contracts;
using SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;
using SPerfomance.Application.EducationPlans.Queries.GetEducationPlan;
using SPerfomance.Application.Semesters.DTO;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;

namespace SPerfomance.Api.Features.Semesters;

public static class GetSemestersByEducationPlan
{
    public record Request(
        EducationDirectionContract Direction,
        EducationPlanContract Plan,
        TokenContract Token
    );

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{SemestersTags.Api}/by-education-plan", Handler)
                .WithTags(SemestersTags.Tag);
    }

    public static async Task<IResult> Handler(
        Request request,
        IUsersRepository users,
        IEducationDirectionRepository repository,
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

        var direction = await new GetEducationDirectionQueryHandler(repository).Handle(
            request.Direction,
            ct
        );
        var plan = await new GetEducationPlanQueryHandler().Handle(
            new GetEducationPlanQuery(direction.Value, request.Plan.PlanYear),
            ct
        );
        if (plan.IsFailure)
            return Results.NotFound(plan.Error.Description);

        return Results.Ok(
            plan.Value.Semesters.Select(s => s.MapFromDomain()).OrderBy(s => s.Number)
        );
    }
}
