using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.EducationDirections.ByPage;

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
