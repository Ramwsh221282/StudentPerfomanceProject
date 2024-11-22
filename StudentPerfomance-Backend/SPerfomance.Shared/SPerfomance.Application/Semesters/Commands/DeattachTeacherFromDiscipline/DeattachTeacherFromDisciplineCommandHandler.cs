using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.SemesterPlans.Abstractions;
using SPerfomance.Domain.Models.SemesterPlans.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Semesters.Commands.DeattachTeacherFromDiscipline;

public class DeattachTeacherFromDisciplineCommandHandler(ISemesterPlansRepository repository)
    : ICommandHandler<DeattachTeacherFromDisciplineCommand, SemesterPlan>
{
    public async Task<Result<SemesterPlan>> Handle(
        DeattachTeacherFromDisciplineCommand command,
        CancellationToken ct = default
    )
    {
        if (command.Discipline == null)
            return Result<SemesterPlan>.Failure(SemesterPlanErrors.NotFound());

        var discipline = command.Discipline.Semester.DeattachTeacherFromDiscipline(
            command.Discipline
        );
        if (discipline.IsFailure)
            return discipline;

        await repository.DeattachTeacherId(discipline.Value, ct);
        return Result<SemesterPlan>.Success(discipline.Value);
    }
}
