using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Queries.Paged;

public sealed class StudentGroupsPaginationService(int page, int pageSize, IRepository<StudentGroup> repository) : IService<IReadOnlyCollection<StudentGroup>>
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	private readonly IRepository<StudentGroup> _repository = repository;
	public async Task<OperationResult<IReadOnlyCollection<StudentGroup>>> DoOperation()
	{
		IReadOnlyCollection<StudentGroup> groups = await _repository.GetPaged(_page, _pageSize);
		return new OperationResult<IReadOnlyCollection<StudentGroup>>(groups);
	}
}
