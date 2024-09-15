using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Group.CreateStudentGroup;

public sealed class StudentGroupCreationService
(
	StudentsGroupSchema schema,
	IRepository<StudentGroup> repository,
	IRepositoryExpression<StudentGroup> expression
)
: IService<StudentGroup>
{
	private readonly CreateStudentGroupCommand _command = new CreateStudentGroupCommand(schema, expression);
	private readonly CreateStudentGroupCommandHandler _handler = new CreateStudentGroupCommandHandler(repository);
	public async Task<OperationResult<StudentGroup>> DoOperation() => await _handler.Handle(_command);
}
