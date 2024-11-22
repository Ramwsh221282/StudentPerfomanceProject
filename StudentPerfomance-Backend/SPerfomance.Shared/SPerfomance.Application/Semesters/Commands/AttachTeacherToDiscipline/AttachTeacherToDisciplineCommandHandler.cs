using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.SemesterPlans.Abstractions;
using SPerfomance.Domain.Models.SemesterPlans.Errors;
using SPerfomance.Domain.Models.Teachers.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Semesters.Commands.AttachTeacherToDiscipline;

public class AttachTeacherToDisciplineCommandHandler(ISemesterPlansRepository repository)
    : ICommandHandler<AttachTeacherToDisciplineCommand, SemesterPlan>
{
    public async Task<Result<SemesterPlan>> Handle(
        AttachTeacherToDisciplineCommand command,
        CancellationToken ct = default
    )
    {
        if (command.Teacher == null)
            return Result<SemesterPlan>.Failure(TeacherErrors.NotFound());

        if (command.Discipline == null)
            return Result<SemesterPlan>.Failure(SemesterPlanErrors.NotFound());

        var plan = command.Discipline.Semester.AttachTeacherToDiscipline(
            command.Discipline,
            command.Teacher
        );
        if (plan.IsFailure)
            return plan;

        await repository.AttachTeacherId(plan.Value, ct);
        return Result<SemesterPlan>.Success(plan.Value);
    }
}
