using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans.Errors;

namespace SPerfomance.Application.EducationPlans.Module.Commands.Delete;

internal sealed class DeleteEducationPlanCommandHandler
(
	IRepository<EducationPlan> repository
) : ICommandHandler<DeleteEducationPlanCommand, OperationResult<EducationPlan>>
{
	private readonly IRepository<EducationPlan> _repository = repository;
	public async Task<OperationResult<EducationPlan>> Handle(DeleteEducationPlanCommand command)
	{
		EducationPlan? plan = await _repository.GetByParameter(command.Expression);
		if (plan == null) return new OperationResult<EducationPlan>(new EducationPlanNotFoundError().ToString());
		await _repository.Remove(plan);
		return new OperationResult<EducationPlan>(plan);
	}
}
