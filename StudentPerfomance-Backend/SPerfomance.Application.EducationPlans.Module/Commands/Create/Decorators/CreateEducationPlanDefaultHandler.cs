using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans.Errors;

namespace SPerfomance.Application.EducationPlans.Module.Commands.Create.Decorators;

internal sealed class CreateEducationPlanDefaultHandler
(
	IRepository<EducationPlan> plans,
	IRepository<EducationDirection> directions
) : ICommandHandler<CreateEducationPlanCommand, OperationResult<EducationPlan>>
{
	private readonly IRepository<EducationPlan> _plans = plans;
	private readonly IRepository<EducationDirection> _directions = directions;
	public async Task<OperationResult<EducationPlan>> Handle(CreateEducationPlanCommand command)
	{
		if (await _plans.HasEqualRecord(command.FindPlanDublicate)) return new OperationResult<EducationPlan>(new EducationPlanDublicateError().ToString());
		if (!command.PlanValidator.IsValid) return new OperationResult<EducationPlan>(command.PlanValidator.Error);
		EducationDirection? direction = await _directions.GetByParameter(command.FindDirection);
		if (direction == null) return new OperationResult<EducationPlan>(new EducationPlanWithoutDirectionError().ToString());
		EducationPlan plan = command.Plan.CreateDomainObject(direction);
		return new OperationResult<EducationPlan>(plan);
	}
}
