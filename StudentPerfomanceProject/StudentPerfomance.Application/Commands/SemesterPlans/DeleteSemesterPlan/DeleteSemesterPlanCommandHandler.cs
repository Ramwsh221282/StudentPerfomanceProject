using StudentPerfomance.Application.EntitySchemas.Validators;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.SemesterPlans.DeleteSemesterPlan;

internal sealed class DeleteSemesterPlanCommandHandler
(
	IRepository<SemesterPlan> repository
)
: CommandWithErrorBuilder, ICommandHandler<DeleteSemesterPlanCommand, OperationResult<SemesterPlan>>
{
	private readonly IRepository<SemesterPlan> _repository = repository;
	public async Task<OperationResult<SemesterPlan>> Handle(DeleteSemesterPlanCommand command)
	{
		SemesterPlan? plan = await _repository.GetByParameter(command.Existance);
		plan.ValidateNullability("План семестра не найден", this);
		return await this.ProcessAsync(async () =>
		{
			await _repository.Remove(plan);
			return new OperationResult<SemesterPlan>(plan);
		});
	}
}
