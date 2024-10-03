using SPerfomance.Application.Shared.Module.Schemas.Students;
using SPerfomance.Application.Shared.Module.Schemas.Students.Validators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Students;
using SPerfomance.Domain.Module.Shared.Entities.Students.Errors;

namespace SPerfomance.Application.Students.Module.Commands.Create;

public sealed class StudentCreateCommand : ICommand
{
	private readonly StudentSchema _student;
	private readonly IRepositoryExpression<Student> _findDublicate;
	private readonly IRepositoryExpression<StudentGroup> _getGroup;
	private readonly ISchemaValidator _validator;
	public readonly ICommandHandler<StudentCreateCommand, Student> Handler;
	public StudentCreateCommand
	(
		StudentSchema student,
		IRepositoryExpression<Student> findDublicate,
		IRepositoryExpression<StudentGroup> getGroup,
		IRepository<Student> students,
		IRepository<StudentGroup> groups
	)
	{
		_student = student;
		_findDublicate = findDublicate;
		_getGroup = getGroup;
		_validator = new StudentValidator()
		.WithNameValidation(student)
		.WithStateValidation(student)
		.WithRecordbookValidation(student);
		_validator.ProcessValidation();
		Handler = new CommandHandler(students, groups);
	}

	internal sealed class CommandHandler(IRepository<Student> students, IRepository<StudentGroup> groups) : ICommandHandler<StudentCreateCommand, Student>
	{
		private readonly IRepository<Student> _students = students;
		private readonly IRepository<StudentGroup> _groups = groups;
		public async Task<OperationResult<Student>> Handle(StudentCreateCommand command)
		{
			if (!command._validator.IsValid)
				return new OperationResult<Student>(command._validator.Error);
			if (await _students.HasEqualRecord(command._findDublicate))
				return new OperationResult<Student>(new StudentDublicateRecordBookError(command._student.Recordbook).ToString());
			StudentGroup? group = await _groups.GetByParameter(command._getGroup);
			if (group == null)
				return new OperationResult<Student>(new GroupNotFoundError().ToString());
			Student student = command._student.CreateDomainObject(group);
			await _students.Create(student);
			return new OperationResult<Student>(student);
		}
	}
}
