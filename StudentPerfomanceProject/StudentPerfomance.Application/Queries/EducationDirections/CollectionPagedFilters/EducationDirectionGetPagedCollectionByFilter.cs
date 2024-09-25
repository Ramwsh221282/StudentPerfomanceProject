using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.EducationDirections.CollectionPagedFilters;

public class EducationDirectionGetPagedCollectionByFilterService
(
	int page,
	int pageSize,
	IRepository<EducationDirection> repository,
	IRepositoryExpression<EducationDirection> expression
) : IService<IReadOnlyCollection<EducationDirection>>
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	private readonly IRepository<EducationDirection> _repository = repository;
	private readonly IRepositoryExpression<EducationDirection> _expression = expression;
	public async Task<OperationResult<IReadOnlyCollection<EducationDirection>>> DoOperation()
	{
		IReadOnlyCollection<EducationDirection> directions = await _repository.GetFilteredAndPaged(_expression, _page, _pageSize);
		return new OperationResult<IReadOnlyCollection<EducationDirection>>(directions);
	}
	protected async Task<OperationResult<IReadOnlyCollection<EducationDirection>>> Process() => await DoOperation();
}
