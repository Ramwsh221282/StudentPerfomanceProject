using StudentPerfomance.Application.EntitySchemas.Validators;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Errors.EducationPlans;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.EducationPlans.Create;

internal sealed class CreateEducationPlanCommandHandler
(
	IRepository<EducationPlan> plans, IRepository<EducationDirection> directions
)
: CommandWithErrorBuilder, ICommandHandler<CreateEducationPlanCommand, OperationResult<EducationPlan>>
{
	private readonly IRepository<EducationPlan> _plans = plans;
	private readonly IRepository<EducationDirection> _directions = directions;

	public async Task<OperationResult<EducationPlan>> Handle(CreateEducationPlanCommand command)
	{
		command.Validator.ValidateSchema(this);
		await _plans.ValidateExistance(command.FindDublicate, new EducationPlanDublicateError().ToString(), this);
		EducationDirection? direction = await _directions.GetByParameter(command.FindDirection);
		direction.ValidateNullability(new EducationPlanWithoutDirectionError().ToString(), this);
		return await this.ProcessAsync(async () =>
		{
			EducationPlan plan = command.Plan.CreateDomainObject(direction);
			await _plans.Create(plan);
			return new OperationResult<EducationPlan>(plan);
		});
	}
}
