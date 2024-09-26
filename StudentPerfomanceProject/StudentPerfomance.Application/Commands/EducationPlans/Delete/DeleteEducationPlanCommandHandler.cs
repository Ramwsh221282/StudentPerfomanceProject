using StudentPerfomance.Application.EntitySchemas.Validators;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Errors.EducationPlans;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.EducationPlans.Delete;

internal sealed class DeleteEducationPlanCommandHandler
(
	IRepository<EducationPlan> repository
) : CommandWithErrorBuilder, ICommandHandler<DeleteEducationPlanCommand, OperationResult<EducationPlan>>
{
	private readonly IRepository<EducationPlan> _repository = repository;
	public async Task<OperationResult<EducationPlan>> Handle(DeleteEducationPlanCommand command)
	{
		EducationPlan? plan = await _repository.GetByParameter(command.Expression);
		plan.ValidateNullability(new EducationPlanNotFoundError().ToString(), this);
		return await this.ProcessAsync(async () =>
		{
			await _repository.Remove(plan);
			return new OperationResult<EducationPlan>(plan);
		});
	}
}
