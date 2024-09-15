using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Group.DeleteStudentGroup;

public sealed class StudentGroupsDeleteService
(
	StudentsGroupSchema schema,
	IRepository<StudentGroup> repository,
	IRepositoryExpression<StudentGroup> expression
)
: IService<StudentGroup>
{
	private readonly DeleteStudentGroupCommand _command = new DeleteStudentGroupCommand(schema, expression);
	private readonly DeleteStudentGroupCommandHandler _handler = new DeleteStudentGroupCommandHandler(repository);
	public async Task<OperationResult<StudentGroup>> DoOperation() => await _handler.Handle(_command);
}
