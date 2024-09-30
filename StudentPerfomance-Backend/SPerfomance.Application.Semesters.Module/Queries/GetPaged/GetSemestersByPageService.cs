using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;

namespace SPerfomance.Application.Semesters.Module.Queries.GetPaged;

public sealed class GetSemestersByPageService(int page, int pageSize, IRepository<Semester> repository) : IService<IReadOnlyCollection<Semester>>
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	private readonly IRepository<Semester> _repository = repository;
	public async Task<OperationResult<IReadOnlyCollection<Semester>>> DoOperation()
	{
		IReadOnlyCollection<Semester> semesters = await _repository.GetPaged(_page, _pageSize);
		return new OperationResult<IReadOnlyCollection<Semester>>(semesters);
	}
}
