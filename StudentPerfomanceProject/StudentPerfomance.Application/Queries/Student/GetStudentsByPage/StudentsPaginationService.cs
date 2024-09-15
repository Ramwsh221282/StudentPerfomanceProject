using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Student.GetStudentsByPage;

public sealed class StudentsPaginationService
(
	int page,
	int pageSize,
	StudentsGroupSchema schema,
	IRepository<Domain.Entities.Student> repository,
	IRepositoryExpression<Domain.Entities.Student> expression
)
: IService<IReadOnlyCollection<Domain.Entities.Student>>
{
	private readonly GetStudentsByPageQuery _query = new GetStudentsByPageQuery(page, pageSize, schema);
	private readonly GetStudentsByPageQueryHandler _handler = new GetStudentsByPageQueryHandler(repository, expression);
	public async Task<OperationResult<IReadOnlyCollection<Domain.Entities.Student>>> DoOperation() => await _handler.Handle(_query);
}
