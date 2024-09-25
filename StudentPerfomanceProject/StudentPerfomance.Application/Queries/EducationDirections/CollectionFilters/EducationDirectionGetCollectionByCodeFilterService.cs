using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.EducationDirections.CollectionFilters;

public sealed class EducationDirectionGetCollectionByCodeFilterService : EducationDirectionGetCollectionByFilter, IService<IReadOnlyCollection<EducationDirection>>
{
	public EducationDirectionGetCollectionByCodeFilterService(IRepository<EducationDirection> repository, IRepositoryExpression<EducationDirection> expression)
	: base(repository, expression)
	{ }

	public new async Task<OperationResult<IReadOnlyCollection<EducationDirection>>> DoOperation() => await Process();
}
