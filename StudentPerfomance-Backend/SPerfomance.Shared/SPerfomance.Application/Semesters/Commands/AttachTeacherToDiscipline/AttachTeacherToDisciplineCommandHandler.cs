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
    private readonly ISemesterPlansRepository _repository = repository;

    public async Task<Result<SemesterPlan>> Handle(AttachTeacherToDisciplineCommand command)
    {
        if (command.Teacher == null)
            return Result<SemesterPlan>.Failure(TeacherErrors.NotFound());

        if (command.Discipline == null)
            return Result<SemesterPlan>.Failure(SemesterPlanErrors.NotFound());

        Result<SemesterPlan> plan = command.Discipline.Semester.AttachTeacherToDiscipline(
            command.Discipline,
            command.Teacher
        );
        if (plan.IsFailure)
            return plan;

        await _repository.AttachTeacherId(plan.Value);
        return Result<SemesterPlan>.Success(plan.Value);
    }
}
