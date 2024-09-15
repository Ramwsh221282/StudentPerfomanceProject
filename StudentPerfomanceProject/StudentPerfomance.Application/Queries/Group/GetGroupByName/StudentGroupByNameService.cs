using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Group.GetGroupByName;

public sealed class StudentGroupByNameService
(
	StudentsGroupSchema schema,
	IRepository<StudentGroup> repository,
	IRepositoryExpression<StudentGroup> expression
)
: IService<StudentGroup>
{
	private readonly GetGroupByNameQuery _query = new GetGroupByNameQuery(schema);
	private readonly GetGroupByNameQueryHandler _handler = new GetGroupByNameQueryHandler(repository, expression);
	public async Task<OperationResult<StudentGroup>> DoOperation() => await _handler.Handle(_query);
}
