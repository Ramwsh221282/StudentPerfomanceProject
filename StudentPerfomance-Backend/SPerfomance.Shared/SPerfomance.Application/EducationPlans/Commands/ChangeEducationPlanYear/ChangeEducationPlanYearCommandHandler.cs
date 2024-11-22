using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Models.EducationPlans.Abstractions;
using SPerfomance.Domain.Models.EducationPlans.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.EducationPlans.Commands.ChangeEducationPlanYear;

public class ChangeEducationPlanYearCommandHandler(IEducationPlansRepository repository)
    : ICommandHandler<ChangeEducationPlanYearCommand, EducationPlan>
{
    public async Task<Result<EducationPlan>> Handle(
        ChangeEducationPlanYearCommand command,
        CancellationToken ct = default
    )
    {
        if (command.Plan == null)
            return Result<EducationPlan>.Failure(EducationPlanErrors.NotFoundError());

        var updated = command.Plan.ChangeYear(command.NewYear);
        if (updated.IsFailure)
            return updated;

        await repository.Update(updated.Value, ct);
        return Result<EducationPlan>.Success(updated.Value);
    }
}
