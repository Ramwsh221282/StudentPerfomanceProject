using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Application.EducationPlans.Module.Commands.Delete;

public class EducationPlanDeletionService
(
	IRepositoryExpression<EducationPlan> expression,
	IRepository<EducationPlan> repository
) : IService<EducationPlan>
{
	private readonly DeleteEducationPlanCommand _command = new DeleteEducationPlanCommand(expression);
	private readonly DeleteEducationPlanCommandHandler _handler = new DeleteEducationPlanCommandHandler(repository);
	public async Task<OperationResult<EducationPlan>> DoOperation() => await _handler.Handle(_command);
}
