using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Models.EducationPlans.Abstractions;
using SPerfomance.Domain.Models.EducationPlans.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.EducationPlans.Commands.RemoveEducationPlan;

public class RemoveEducationPlanCommandHandler(IEducationPlansRepository repository)
    : ICommandHandler<RemoveEducationPlanCommand, EducationPlan>
{
    public async Task<Result<EducationPlan>> Handle(
        RemoveEducationPlanCommand command,
        CancellationToken ct = default
    )
    {
        if (command.Plan == null)
            return Result<EducationPlan>.Failure(EducationPlanErrors.NotFoundError());

        await repository.Remove(command.Plan, ct);
        return Result<EducationPlan>.Success(command.Plan);
    }
}
