using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Application.EntitySchemas.Schemas.Students;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Student.CreateStudent;

public sealed class StudentCreationService
(
	StudentSchema student,
	StudentsGroupSchema group,
	IRepository<Domain.Entities.Student> studentsRepository,
	IRepository<StudentGroup> groupsRepository,
	IRepositoryExpression<StudentGroup> hasGroup,
	IRepositoryExpression<Domain.Entities.Student> hasStudent,
	IRepositoryExpression<Domain.Entities.Student> recordBookExists
)
: IService<Domain.Entities.Student>
{
	private readonly CreateStudentCommand _command = new CreateStudentCommand(student, group);
	private readonly CreateStudentCommandHandler _handler = new CreateStudentCommandHandler
	(
		studentsRepository,
		groupsRepository,
		hasGroup,
		hasStudent,
		recordBookExists
	);
	public async Task<OperationResult<Domain.Entities.Student>> DoOperation() => await _handler.Handle(_command);
}
