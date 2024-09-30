using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Application.EducationPlans.Module.Queries.Search;

public class EducationPlanSearchByFilter
(
	IRepository<EducationPlan> repository,
	IRepositoryExpression<EducationPlan> expression
) : IService<IReadOnlyCollection<EducationPlan>>
{
	private readonly IRepository<EducationPlan> _repository = repository;
	private readonly IRepositoryExpression<EducationPlan> _expression = expression;
	public async Task<OperationResult<IReadOnlyCollection<EducationPlan>>> DoOperation()
	{
		IReadOnlyCollection<EducationPlan> plans = await _repository.GetFiltered(_expression);
		return new OperationResult<IReadOnlyCollection<EducationPlan>>(plans);
	}
}
