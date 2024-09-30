using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Application.EducationPlans.Module.Queries.PagedFilters;

public class EducationPlanGetPagedByFilter
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
}
