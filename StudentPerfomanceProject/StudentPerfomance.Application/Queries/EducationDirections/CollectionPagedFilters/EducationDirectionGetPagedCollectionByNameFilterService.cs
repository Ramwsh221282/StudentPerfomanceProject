using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.EducationDirections.CollectionPagedFilters;

public sealed class EducationDirectionGetPagedCollectionByNameFilterService : EducationDirectionGetPagedCollectionByFilterService, IService<IReadOnlyCollection<EducationDirection>>
{
	public EducationDirectionGetPagedCollectionByNameFilterService(int page, int pageSize, IRepository<EducationDirection> repository, IRepositoryExpression<EducationDirection> expression)
	: base(page, pageSize, repository, expression)
	{ }

	public new async Task<OperationResult<IReadOnlyCollection<EducationDirection>>> DoOperation() => await Process();
}
