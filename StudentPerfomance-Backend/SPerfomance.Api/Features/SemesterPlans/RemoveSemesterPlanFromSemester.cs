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
using SPerfomance.Application.Semesters.GetSemester.Queries;
using SPerfomance.Application.Semesters.Queries.GetDisciplineFromSemester;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;
using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.SemesterPlans.Abstractions;
using SPerfomance.Domain.Models.Semesters;
using SPerfomance.Domain.Tools;

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
        ISemesterPlansRepository semesterPlans
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
        Result<EducationPlan> plan = await new GetEducationPlanQueryHandler().Handle(
            new(direction.Value, request.Plan.PlanYear)
        );
        Result<Semester> semester = await new GetSemesterQueryHandler().Handle(
            new(plan.Value, request.Semester.Number)
        );
        Result<SemesterPlan> discipline = await new GetDisciplineFromSemesterQueryHandler().Handle(
            new(semester.Value, request.Discipline.Discipline)
        );
        discipline = await new RemoveDisciplineCommandHandler(semesterPlans).Handle(
            new(discipline.Value)
        );

        return discipline.IsFailure
            ? Results.BadRequest(discipline.Error.Description)
            : Results.Ok(discipline.Value.MapFromDomain());
    }
}
