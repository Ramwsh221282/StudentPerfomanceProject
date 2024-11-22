using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.EducationDirections.Contracts;
using SPerfomance.Api.Features.EducationPlans.Contracts;
using SPerfomance.Api.Features.SemesterPlans.Contracts;
using SPerfomance.Api.Features.Semesters.Contracts;
using SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;
using SPerfomance.Application.EducationPlans.Queries.GetEducationPlan;
using SPerfomance.Application.Semesters.Commands.RemoveDiscipline;
using SPerfomance.Application.Semesters.DTO;
using SPerfomance.Application.Semesters.Queries.GetDisciplineFromSemester;
using SPerfomance.Application.Semesters.Queries.GetSemester;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;
using SPerfomance.Domain.Models.SemesterPlans.Abstractions;

namespace SPerfomance.Api.Features.SemesterPlans;

public static class RemoveSemesterPlanFromSemester
{
    public record Request(
        EducationDirectionContract Direction,
        EducationPlanContract Plan,
        SemesterContract Semester,
        SemesterPlanContract Discipline,
        TokenContract Token
    );

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapDelete($"{SemesterPlanTags.Api}", Handler).WithTags(SemesterPlanTags.Tag);
    }

    public static async Task<IResult> Handler(
        [FromBody] Request request,
        IUsersRepository users,
        IEducationDirectionRepository directions,
        ISemesterPlansRepository semesterPlans,
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
        var semester = await new GetSemesterQueryHandler().Handle(
            new GetSemesterQuery(plan.Value, request.Semester.Number),
            ct
        );
        var discipline = await new GetDisciplineFromSemesterQueryHandler().Handle(
            new GetDisciplineFromSemesterQuery(semester.Value, request.Discipline.Discipline),
            ct
        );
        discipline = await new RemoveDisciplineCommandHandler(semesterPlans).Handle(
            new RemoveDisciplineCommand(discipline.Value),
            ct
        );

        return discipline.IsFailure
            ? Results.BadRequest(discipline.Error.Description)
            : Results.Ok(discipline.Value.MapFromDomain());
    }
}
