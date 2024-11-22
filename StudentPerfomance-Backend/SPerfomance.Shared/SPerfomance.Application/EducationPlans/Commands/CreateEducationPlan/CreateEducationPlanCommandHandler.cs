using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Models.EducationPlans.Abstractions;
using SPerfomance.Domain.Models.EducationPlans.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.EducationPlans.Commands.CreateEducationPlan;

public class CreateEducationPlanCommandHandler(IEducationPlansRepository repository)
    : ICommandHandler<CreateEducationPlanCommand, EducationPlan>
{
    public async Task<Result<EducationPlan>> Handle(
        CreateEducationPlanCommand command,
        CancellationToken ct = default
    )
    {
        if (command.Direction == null)
            return Result<EducationPlan>.Failure(EducationPlanErrors.WithoutDirection());

        var plan = command.Direction.AddEducationPlan(command.Year);
        if (plan.IsFailure)
            return plan;

        await repository.Insert(plan.Value, ct);
        return Result<EducationPlan>.Success(plan.Value);
    }
}
