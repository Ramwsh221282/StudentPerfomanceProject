using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Application.Teachers.Module.Queries.GetPaged;

public sealed class GetPagedTeachersService(int page, int pageSize, IRepository<Teacher> repository) : IService<IReadOnlyCollection<Teacher>>
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	private readonly IRepository<Teacher> _repository = repository;
	public async Task<OperationResult<IReadOnlyCollection<Teacher>>> DoOperation()
	{
		IReadOnlyCollection<Teacher> teachers = await _repository.GetPaged(_page, _pageSize);
		return new OperationResult<IReadOnlyCollection<Teacher>>(teachers);
	}
}
