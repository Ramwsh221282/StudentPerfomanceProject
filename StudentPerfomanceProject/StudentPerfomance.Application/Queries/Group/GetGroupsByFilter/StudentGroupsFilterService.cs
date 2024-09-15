using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Group.GetGroupsByFilter;

public sealed class StudentGroupsFilterService
(
	StudentsGroupSchema schema,
	int page,
	int pageSize,
	IRepository<StudentGroup> repository,
	IRepositoryExpression<StudentGroup> expression
)
: IService<IReadOnlyCollection<StudentGroup>>
{
	private readonly GetGroupsByFilterQuery _query = new GetGroupsByFilterQuery(schema, page, pageSize);
	private readonly GetGroupsByFilterQueryHandler _handler = new GetGroupsByFilterQueryHandler(repository, expression);
	public async Task<OperationResult<IReadOnlyCollection<StudentGroup>>> DoOperation() => await _handler.Handle(_query);
}
