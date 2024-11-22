using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.EducationDirections.Contracts;
using SPerfomance.Api.Features.EducationPlans.Contracts;
using SPerfomance.Api.Features.Semesters.Contracts;
using SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;
using SPerfomance.Application.EducationPlans.Queries.GetEducationPlan;
using SPerfomance.Application.Semesters.DTO;
using SPerfomance.Application.Semesters.Queries.GetSemester;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;

namespace SPerfomance.Api.Features.SemesterPlans;

public static class GetSemesterPlansBySemester
{
    public record Request(
        EducationDirectionContract Direction,
        EducationPlanContract Plan,
        SemesterContract Semester,
        TokenContract Token
    );

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{SemesterPlanTags.Api}/get-by-semester", Handler)
                .WithTags(SemesterPlanTags.Tag);
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
        var educationPlan = await new GetEducationPlanQueryHandler().Handle(
            new GetEducationPlanQuery(direction.Value, request.Plan.PlanYear),
            ct
        );
        var semester = await new GetSemesterQueryHandler().Handle(
            new GetSemesterQuery(educationPlan.Value, request.Semester.Number),
            ct
        );

        return semester.IsFailure
            ? Results.BadRequest(semester.Error.Description)
            : Results.Ok(semester.Value.Disciplines.Select(d => d.MapFromDomain()));
    }
}
