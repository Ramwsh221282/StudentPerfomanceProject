using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.EducationDirections.CollectionFilters;

public class EducationDirectionGetCollectionByFilter
(
	IRepository<EducationDirection> repository,
	IRepositoryExpression<EducationDirection> expression
) : IService<IReadOnlyCollection<EducationDirection>>
{
	private readonly IRepository<EducationDirection> _repository = repository;
	private readonly IRepositoryExpression<EducationDirection> _expression = expression;
	public async Task<OperationResult<IReadOnlyCollection<EducationDirection>>> DoOperation()
	{
		IReadOnlyCollection<EducationDirection> directions = await _repository.GetFiltered(_expression);
		return new OperationResult<IReadOnlyCollection<EducationDirection>>(directions);
	}
	protected async Task<OperationResult<IReadOnlyCollection<EducationDirection>>> Process() => await DoOperation();
}
