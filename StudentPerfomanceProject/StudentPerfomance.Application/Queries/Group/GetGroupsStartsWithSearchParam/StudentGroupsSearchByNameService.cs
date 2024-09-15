using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Group.GetGroupsStartsWithSearchParam;

public sealed class StudentGroupsSearchByNameService
(
	StudentsGroupSchema schema,
	IRepository<StudentGroup> repository,
	IRepositoryExpression<StudentGroup> expression
)
: IService<IReadOnlyCollection<StudentGroup>>
{
	private readonly GetGroupsStartWithSearchParamQuery _query = new GetGroupsStartWithSearchParamQuery(schema);
	private readonly GetGroupsStartWithSearchParamQueryHandler _handler = new GetGroupsStartWithSearchParamQueryHandler(repository, expression);
	public async Task<OperationResult<IReadOnlyCollection<StudentGroup>>> DoOperation() => await _handler.Handle(_query);
}
