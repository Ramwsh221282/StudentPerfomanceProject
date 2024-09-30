using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Queries.PagedFiltered;

public sealed class StudentGroupsFilterService
(
	int page,
	int pageSize,
	IRepositoryExpression<StudentGroup> expression,
	IRepository<StudentGroup> repository
) : IService<IReadOnlyCollection<StudentGroup>>
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	private readonly IRepositoryExpression<StudentGroup> _expression = expression;
	private readonly IRepository<StudentGroup> _repository = repository;
	public async Task<OperationResult<IReadOnlyCollection<StudentGroup>>> DoOperation()
	{
		IReadOnlyCollection<StudentGroup> groups = await _repository.GetFilteredAndPaged(_expression, _page, _pageSize);
		return new OperationResult<IReadOnlyCollection<StudentGroup>>(groups);
	}
}
