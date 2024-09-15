using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Group.ChangeGroupName;

public sealed class StudentGroupsUpdateService
(
	StudentsGroupSchema schema,
	IRepository<StudentGroup> repository,
	IRepositoryExpression<StudentGroup> existance,
	IRepositoryExpression<StudentGroup> dublicate
)
: IService<StudentGroup>
{
	private readonly ChangeGroupNameCommand _command = new ChangeGroupNameCommand(schema, existance, dublicate);
	private readonly ChangeGroupNameCommandHandler _handler = new ChangeGroupNameCommandHandler(repository);
	public async Task<OperationResult<StudentGroup>> DoOperation() => await _handler.Handle(_command);
}
