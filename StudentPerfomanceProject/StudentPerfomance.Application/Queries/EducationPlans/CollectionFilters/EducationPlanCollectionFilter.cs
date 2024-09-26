using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.EducationPlans.CollectionFilters;

public class EducationPlanCollectionFilter
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

	protected async Task<OperationResult<IReadOnlyCollection<EducationPlan>>> Process() => await DoOperation();
}
