using StudentPerfomance.Application.EntitySchemas.Schemas.Students;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Student.DeleteStudent;

public sealed class StudentDeletionService
(
	StudentSchema schema,
	IRepository<Domain.Entities.Student> repository,
	IRepositoryExpression<Domain.Entities.Student> expression
)
: IService<Domain.Entities.Student>
{
	private readonly DeleteStudentCommand _command = new DeleteStudentCommand(schema);
	private readonly DeleteStudentCommandHandler _handler = new DeleteStudentCommandHandler(repository, expression);
	public async Task<OperationResult<Domain.Entities.Student>> DoOperation() => await _handler.Handle(_command);
}
