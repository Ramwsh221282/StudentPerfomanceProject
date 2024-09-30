using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Application.Teachers.Module.Queries.Search;

public sealed class SearchTeachersByFilterService
(
	IRepositoryExpression<Teacher> expression,
	IRepository<Teacher> repository
) : IService<IReadOnlyCollection<Teacher>>
{
	private readonly IRepositoryExpression<Teacher> _expression = expression;
	private readonly IRepository<Teacher> _repository = repository;
	public async Task<OperationResult<IReadOnlyCollection<Teacher>>> DoOperation()
	{
		IReadOnlyCollection<Teacher> teachers = await _repository.GetFiltered(_expression);
		return new OperationResult<IReadOnlyCollection<Teacher>>(teachers);
	}
}
