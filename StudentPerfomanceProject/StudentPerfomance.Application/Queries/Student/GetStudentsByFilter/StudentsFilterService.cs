using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Student.GetStudentsByFilter;

public sealed class StudentsFilterService
(
	int page,
	int pageSize,
	IRepository<Domain.Entities.Student> repository,
	IRepositoryExpression<Domain.Entities.Student> expression
)
: IService<IReadOnlyCollection<Domain.Entities.Student>>
{
	private readonly GetStudentsByFilterQuery _query = new GetStudentsByFilterQuery(page, pageSize);
	private readonly GetStudentsByFilterQueryHandler _handler = new GetStudentsByFilterQueryHandler(repository, expression);
	public async Task<OperationResult<IReadOnlyCollection<Domain.Entities.Student>>> DoOperation() => await _handler.Handle(_query);
}
