using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.Students;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.StudentGroups.Commands.UpdateStudent;

public class UpdateStudentCommand
(
	Student? student,
	string? newName,
	string? newSurname,
	string? newPatronymic,
	string? newState,
	ulong? newRecordbook,
	string? newGroup
)
: ICommand<Student>
{
	public Student? Student { get; init; } = student;

	public string NewName { get; init; } = newName.ValueOrEmpty();

	public string NewSurname { get; init; } = newSurname.ValueOrEmpty();

	public string NewPatronymic { get; init; } = newPatronymic.ValueOrEmpty();

	public string NewState { get; init; } = newState.ValueOrEmpty();

	public ulong NewRecordBook { get; init; } = newRecordbook.ValueOrEmpty();

	public string NewGroup { get; init; } = newGroup.ValueOrEmpty();
}
