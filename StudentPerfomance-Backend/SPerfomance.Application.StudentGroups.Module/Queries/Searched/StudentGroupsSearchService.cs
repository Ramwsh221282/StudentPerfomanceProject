using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Queries.Searched;

public sealed class StudentGroupsSearchService
(
	IRepository<StudentGroup> repository,
	IRepositoryExpression<StudentGroup> expression
) : IService<IReadOnlyCollection<StudentGroup>>
{
	private readonly IRepository<StudentGroup> _repository = repository;
	private readonly IRepositoryExpression<StudentGroup> _expression = expression;
	public async Task<OperationResult<IReadOnlyCollection<StudentGroup>>> DoOperation()
	{
		IReadOnlyCollection<StudentGroup> groups = await _repository.GetFiltered(_expression);
		return new OperationResult<IReadOnlyCollection<StudentGroup>>(groups);
	}
}
