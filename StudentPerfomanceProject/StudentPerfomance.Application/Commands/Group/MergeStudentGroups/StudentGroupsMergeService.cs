using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Group.MergeStudentGroups;

public sealed class StudentGroupsMergeService
(
	StudentsGroupSchema groupA,
	StudentsGroupSchema groupB,
	IRepository<StudentGroup> repository,
	IRepositoryExpression<StudentGroup> existanceA,
	IRepositoryExpression<StudentGroup> existanceB
)
: IService<StudentGroup>
{
	private readonly MergeStudentGroupsCommand _command = new MergeStudentGroupsCommand(groupA, groupB, existanceA, existanceB);
	private readonly MergeStudentGroupsCommandHandler _handler = new MergeStudentGroupsCommandHandler(repository);
	public async Task<OperationResult<StudentGroup>> DoOperation() => await _handler.Handle(_command);
}
