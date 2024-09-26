using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.EducationPlans.CollectionPagedFilters;

public class EducationPlanPagedCollectionFilter
(
	int page,
	int pageSize,
	IRepository<EducationPlan> repository,
	IRepositoryExpression<EducationPlan> expression
) : IService<IReadOnlyCollection<EducationPlan>>
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	private readonly IRepository<EducationPlan> _repository = repository;
	private readonly IRepositoryExpression<EducationPlan> _expression = expression;
	public async Task<OperationResult<IReadOnlyCollection<EducationPlan>>> DoOperation()
	{
		IReadOnlyCollection<EducationPlan> plans = await _repository.GetFilteredAndPaged(_expression, _page, _pageSize);
		return new OperationResult<IReadOnlyCollection<EducationPlan>>(plans);
	}
	protected async Task<OperationResult<IReadOnlyCollection<EducationPlan>>> Process() => await DoOperation();
}
