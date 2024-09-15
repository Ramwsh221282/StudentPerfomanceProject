using StudentPerfomance.Application.EntitySchemas.Validators;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;
using StudentPerfomance.Domain.ValueObjects;
using StudentPerfomance.Domain.ValueObjects.Student;

namespace StudentPerfomance.Application.Commands.Student.ChangeStudentData;

internal sealed class ChangeStudentDataCommandHandler
(
	 IRepository<Domain.Entities.Student> studentsRepository,
	 IRepository<StudentGroup> groupsRepository,
	 IRepositoryExpression<Domain.Entities.Student> studentExistance,
	 IRepositoryExpression<Domain.Entities.Student> recordBookDublicate,
	 IRepositoryExpression<StudentGroup> groupExistance
)
: CommandWithErrorBuilder, ICommandHandler<ChangeStudentDataCommand, OperationResult<Domain.Entities.Student>>
{
	private readonly IRepository<Domain.Entities.Student> _studentsRepository = studentsRepository;
	private readonly IRepository<StudentGroup> _groupsRepository = groupsRepository;
	private readonly IRepositoryExpression<Domain.Entities.Student> _studentExistance = studentExistance;
	private readonly IRepositoryExpression<Domain.Entities.Student> _recordBookDublicate = recordBookDublicate;
	private readonly IRepositoryExpression<StudentGroup> _groupExistance = groupExistance;
	public async Task<OperationResult<Domain.Entities.Student>> Handle(ChangeStudentDataCommand command)
	{
		command.StudentValidator.ProcessValidation();
		command.GroupValidator.ProcessValidation();
		Domain.Entities.Student? student = await _studentsRepository.GetByParameter(_studentExistance);
		student.ValidateNullability("Студент не найден", this);
		if (student != null && !student.Recordbook.IsSameAs(command.Student.Recordbook))
			await _studentsRepository.ValidateExistance(_recordBookDublicate, "Студент с такой зачётной книжкой уже существует", this);
		StudentGroup? group = await _groupsRepository.GetByParameter(_groupExistance);
		group.ValidateNullability("Группа не найдена", this);
		return await this.ProcessAsync(async () =>
		{
			StudentName name = StudentName.Create(command.Student.Name, command.Student.Surname, command.Student.Thirdname).Value;
			StudentState state = StudentState.Create(command.Student.State).Value;
			StudentRecordBook recordBook = StudentRecordBook.Create(command.Student.Recordbook).Value;
			student.ChangeName(name);
			student.ChangeState(state);
			student.ChangeRecordBook(recordBook);
			await _studentsRepository.Commit();
			return new OperationResult<Domain.Entities.Student>(student);
		});
	}
}
