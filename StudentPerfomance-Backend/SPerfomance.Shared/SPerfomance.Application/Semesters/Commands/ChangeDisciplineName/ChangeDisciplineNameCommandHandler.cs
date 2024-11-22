using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.SemesterPlans.Abstractions;
using SPerfomance.Domain.Models.SemesterPlans.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Semesters.Commands.ChangeDisciplineName;

public class ChangeDisciplineNameCommandHandler(ISemesterPlansRepository repository)
    : ICommandHandler<ChangeDisciplineNameCommand, SemesterPlan>
{
    public async Task<Result<SemesterPlan>> Handle(
        ChangeDisciplineNameCommand command,
        CancellationToken ct = default
    )
    {
        if (command.Discipline == null)
            return Result<SemesterPlan>.Failure(SemesterPlanErrors.NotFound());

        var updated = command.Discipline.Semester.ChangeDisciplineName(
            command.Discipline,
            command.NewName
        );
        if (updated.IsFailure)
            return updated;

        await repository.Update(updated.Value, ct);
        return updated;
    }
}
