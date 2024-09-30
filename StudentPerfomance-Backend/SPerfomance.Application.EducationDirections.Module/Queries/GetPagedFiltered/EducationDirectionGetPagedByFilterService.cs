using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Queries.GetPagedFiltered;

public sealed class EducationDirectionGetPagedByFilterService
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
}
