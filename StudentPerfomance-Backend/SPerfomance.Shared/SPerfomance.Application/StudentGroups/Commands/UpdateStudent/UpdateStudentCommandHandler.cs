using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Models.StudentGroups.Errors;
using SPerfomance.Domain.Models.Students;
using SPerfomance.Domain.Models.Students.Abstractions;
using SPerfomance.Domain.Models.Students.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.StudentGroups.Commands.UpdateStudent;

public class UpdateStudentCommandHandler
(
	IStudentsRepository students,
	IStudentGroupsRepository groups
)
: ICommandHandler<UpdateStudentCommand, Student>
{
	private readonly IStudentsRepository _students = students;

	private readonly IStudentGroupsRepository _groups = groups;

	public async Task<Result<Student>> Handle(UpdateStudentCommand command)
	{
		if (command.Student == null)
			return Result<Student>.Failure(StudentErrors.NotFound());

		Result<Student> updatedStudent = command.Student
		.ChangeName(command.NewName, command.NewSurname, command.NewPatronymic);
		if (updatedStudent.IsFailure)
			return updatedStudent;

		updatedStudent = updatedStudent.Value.ChangeState(command.NewState);
		if (updatedStudent.IsFailure)
			return updatedStudent;

		updatedStudent = updatedStudent.Value.ChangeRecordBook(command.NewRecordBook);
		if (updatedStudent.IsFailure)
			return updatedStudent;

		bool isGroupChanged = false;
		if (updatedStudent.Value.AttachedGroup.Name.Name != command.NewGroup)
		{
			StudentGroup? newGroup = await _groups.Get(command.NewGroup);
			if (newGroup == null)
				return Result<Student>.Failure(StudentGroupErrors.NotFound());
			updatedStudent.Value.ChangeGroup(newGroup);
			isGroupChanged = true;
		}

		if (isGroupChanged)
			await _students.UpdateWithGroupId(updatedStudent.Value);

		if (!isGroupChanged)
			await _students.Update(updatedStudent.Value);

		return updatedStudent;
	}
}
