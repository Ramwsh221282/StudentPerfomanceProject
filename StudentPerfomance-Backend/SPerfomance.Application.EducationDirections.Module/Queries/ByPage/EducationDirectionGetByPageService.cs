using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Queries.ByPage;

public sealed class EducationDirectionGetByPageService
(
	int page,
	int pageSize,
	IRepository<EducationDirection> repository
) : IService<IReadOnlyCollection<EducationDirection>>
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	private readonly IRepository<EducationDirection> _repository = repository;
	public async Task<OperationResult<IReadOnlyCollection<EducationDirection>>> DoOperation()
	{
		IReadOnlyCollection<EducationDirection> directions = await _repository.GetPaged(_page, _pageSize);
		return new OperationResult<IReadOnlyCollection<EducationDirection>>(directions);
	}
}
