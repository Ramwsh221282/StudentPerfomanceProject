using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Models.EducationPlans.Abstractions;
using SPerfomance.Domain.Models.EducationPlans.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.EducationPlans.Commands.CreateEducationPlan;

public class CreateEducationPlanCommandHandler(IEducationPlansRepository repository)
    : ICommandHandler<CreateEducationPlanCommand, EducationPlan>
{
    private readonly IEducationPlansRepository _repository = repository;

    public async Task<Result<EducationPlan>> Handle(CreateEducationPlanCommand command)
    {
        if (command.Direction == null)
            return Result<EducationPlan>.Failure(EducationPlanErrors.WithoutDirection());

        Result<EducationPlan> plan = command.Direction.AddEducationPlan(command.Year);
        if (plan.IsFailure)
            return plan;

        await _repository.Insert(plan.Value);
        return Result<EducationPlan>.Success(plan.Value);
    }
}
