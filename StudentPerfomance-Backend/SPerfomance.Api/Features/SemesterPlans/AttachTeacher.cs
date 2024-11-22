using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.EducationDirections.Contracts;
using SPerfomance.Api.Features.EducationPlans.Contracts;
using SPerfomance.Api.Features.SemesterPlans.Contracts;
using SPerfomance.Api.Features.Semesters.Contracts;
using SPerfomance.Api.Features.TeacherDepartments.Contracts;
using SPerfomance.Api.Features.Teachers.Contracts;
using SPerfomance.Application.Departments.Queries.GetDepartmentByName;
using SPerfomance.Application.Departments.Queries.GetTeacherFromDepartment;
using SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;
using SPerfomance.Application.EducationPlans.Queries.GetEducationPlan;
using SPerfomance.Application.Semesters.Commands.AttachTeacherToDiscipline;
using SPerfomance.Application.Semesters.DTO;
using SPerfomance.Application.Semesters.Queries.GetDisciplineFromSemester;
using SPerfomance.Application.Semesters.Queries.GetSemester;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;
using SPerfomance.Domain.Models.SemesterPlans.Abstractions;
using SPerfomance.Domain.Models.TeacherDepartments.Abstractions;

namespace SPerfomance.Api.Features.SemesterPlans;

public static class AttachTeacher
{
    public record Request(
        EducationDirectionContract Direction,
        EducationPlanContract Plan,
        SemesterContract Semester,
        SemesterPlanContract Discipline,
        TeacherDepartmentContract Department,
        TeacherContract Teacher,
        TokenContract Token
    );

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{SemesterPlanTags.Api}/attach-teacher", Handler)
                .WithTags(SemesterPlanTags.Tag);
    }

    public static async Task<IResult> Handler(
        Request request,
        IUsersRepository users,
        ITeacherDepartmentsRepository departments,
        IEducationDirectionRepository directions,
        ISemesterPlansRepository disciplines,
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

        var department = await new GetDepartmentByNameQueryHandler(departments).Handle(
            request.Department,
            ct
        );
        var teacher = await new GetTeacherFromDepartmentQueryHandler().Handle(
            new GetTeacherFromDepartmentQuery(
                department.Value,
                request.Teacher.Name,
                request.Teacher.Surname,
                request.Teacher.Patronymic,
                request.Teacher.Job,
                request.Teacher.State
            ),
            ct
        );

        var direction = await new GetEducationDirectionQueryHandler(directions).Handle(
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
        var discipline = await new GetDisciplineFromSemesterQueryHandler().Handle(
            new GetDisciplineFromSemesterQuery(semester.Value, request.Discipline.Discipline),
            ct
        );
        discipline = await new AttachTeacherToDisciplineCommandHandler(disciplines).Handle(
            new AttachTeacherToDisciplineCommand(teacher.Value, discipline.Value),
            ct
        );

        return discipline.IsFailure
            ? Results.BadRequest(discipline.Error.Description)
            : Results.Ok(discipline.Value.MapFromDomain());
    }
}
