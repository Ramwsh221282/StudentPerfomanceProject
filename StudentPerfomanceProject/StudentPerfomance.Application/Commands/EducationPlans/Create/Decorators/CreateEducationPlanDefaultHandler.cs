using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Errors.EducationPlans;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.EducationPlans.Create.Decorators;

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
		if (!command.PlanValidator.IsValid) return new OperationResult<EducationPlan>(command.PlanValidator.Error);
		if (await _plans.HasEqualRecord(command.FindPlanDublicate)) return new OperationResult<EducationPlan>(new EducationPlanDublicateError().ToString());
		EducationDirection? direction = await _directions.GetByParameter(command.FindDirection);
		if (direction == null) return new OperationResult<EducationPlan>(new EducationPlanWithoutDirectionError().ToString());
		EducationPlan plan = command.Plan.CreateDomainObject(direction);
		return new OperationResult<EducationPlan>(plan);
	}
}
