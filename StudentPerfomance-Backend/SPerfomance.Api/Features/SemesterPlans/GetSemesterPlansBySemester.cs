using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.EducationDirections.Contracts;
using SPerfomance.Api.Features.EducationPlans.Contracts;
using SPerfomance.Api.Features.Semesters.Contracts;
using SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;
using SPerfomance.Application.EducationPlans.Queries.GetEducationPlan;
using SPerfomance.Application.Semesters.DTO;
using SPerfomance.Application.Semesters.GetSemester.Queries;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;
using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Models.Semesters;
using SPerfomance.Domain.Tools;

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
        IEducationDirectionRepository repository
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
            repository
        ).Handle(request.Direction);
        Result<EducationPlan> educationPlan = await new GetEducationPlanQueryHandler().Handle(
            new(direction.Value, request.Plan.PlanYear)
        );
        Result<Semester> semester = await new GetSemesterQueryHandler().Handle(
            new(educationPlan.Value, request.Semester.Number)
        );

        if (semester.IsFailure)
            return Results.BadRequest(semester.Error.Description);

        return Results.Ok(semester.Value.Disciplines.Select(d => d.MapFromDomain()));
    }
}
