using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Application.EntitySchemas.Schemas.Students;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Student.ChangeStudentData;

public sealed class StudentUpdateService
(
	StudentSchema student,
	StudentsGroupSchema group,
	IRepository<Domain.Entities.Student> studentsRepository,
	IRepository<StudentGroup> groupsRepository,
	IRepositoryExpression<Domain.Entities.Student> studentExistance,
	IRepositoryExpression<Domain.Entities.Student> recordBookDublicate,
	IRepositoryExpression<StudentGroup> groupExistance
)
: IService<Domain.Entities.Student>
{
	private readonly ChangeStudentDataCommand _command = new ChangeStudentDataCommand(student, group);
	private readonly ChangeStudentDataCommandHandler _handler = new ChangeStudentDataCommandHandler
	(
		studentsRepository,
		groupsRepository,
		studentExistance,
		recordBookDublicate,
		groupExistance
	);
	public async Task<OperationResult<Domain.Entities.Student>> DoOperation() => await _handler.Handle(_command);
}
