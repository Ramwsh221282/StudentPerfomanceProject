using StudentPerfomance.Application.EntitySchemas.Validators;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;
using StudentPerfomance.Domain.ValueObjects;
using StudentPerfomance.Domain.ValueObjects.Student;

namespace StudentPerfomance.Application.Commands.Student.CreateStudent;

internal sealed class CreateStudentCommandHandler
(
	IRepository<Domain.Entities.Student> studentsRepository,
	IRepository<StudentGroup> groupsRepository,
	IRepositoryExpression<StudentGroup> groupExpression,
	IRepositoryExpression<Domain.Entities.Student> dublicateStudent,
	IRepositoryExpression<Domain.Entities.Student> dublicateRecordBookExpression
)
: CommandWithErrorBuilder, ICommandHandler<CreateStudentCommand, OperationResult<Domain.Entities.Student>>
{
	private readonly IRepository<Domain.Entities.Student> _studentsRepository = studentsRepository;
	private readonly IRepository<StudentGroup> _groupsRepository = groupsRepository;
	private readonly IRepositoryExpression<StudentGroup> _groupExpression = groupExpression;
	private readonly IRepositoryExpression<Domain.Entities.Student> _dublicateStudent = dublicateStudent;
	private readonly IRepositoryExpression<Domain.Entities.Student> _dublicateRecordBookExpression = dublicateRecordBookExpression;

	public async Task<OperationResult<Domain.Entities.Student>> Handle(CreateStudentCommand command)
	{
		command.StudentValidation.ValidateSchema(this);
		command.GroupValidation.ValidateSchema(this);
		StudentGroup? group = await _groupsRepository.GetByParameter(_groupExpression);
		group.ValidateNullability("Группа не найдена", this);
		await _studentsRepository.ValidateExistance(_dublicateStudent, "Невозможно создать дубликат студента", this);
		await _studentsRepository.ValidateExistance(_dublicateRecordBookExpression, "Студент с такой зачётной книжкой уже существует", this);
		return await this.ProcessAsync(async () =>
		{
			StudentName name = StudentName.Create(command.Student.Name, command.Student.Surname, command.Student.Thirdname).Value;
			StudentState state = StudentState.Create(command.Student.State).Value;
			StudentRecordBook recordBook = StudentRecordBook.Create(command.Student.Recordbook).Value;
			Domain.Entities.Student student = Domain.Entities.Student.Create(Guid.NewGuid(), name, state, recordBook, group).Value;
			await _studentsRepository.Create(student);
			return new OperationResult<Domain.Entities.Student>(student);
		});
	}
}
