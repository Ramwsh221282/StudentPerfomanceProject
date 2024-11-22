using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.SemesterPlans.Abstractions;
using SPerfomance.Domain.Models.SemesterPlans.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Semesters.Commands.RemoveDiscipline;

public class RemoveDisciplineCommandHandler(ISemesterPlansRepository repository)
    : ICommandHandler<RemoveDisciplineCommand, SemesterPlan>
{
    public async Task<Result<SemesterPlan>> Handle(
        RemoveDisciplineCommand command,
        CancellationToken ct = default
    )
    {
        if (command.Discipline == null)
            return Result<SemesterPlan>.Failure(SemesterPlanErrors.NotFound());

        var result = command.Discipline.Semester.RemoveDiscipline(command.Discipline);

        if (!result.IsFailure)
            await repository.Remove(command.Discipline, ct);

        return result;
    }
}
