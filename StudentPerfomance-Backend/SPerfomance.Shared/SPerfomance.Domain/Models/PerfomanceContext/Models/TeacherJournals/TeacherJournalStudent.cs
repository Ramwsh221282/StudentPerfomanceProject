using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments.ValueObjects;
using SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.TeacherJournals;

public class TeacherJournalStudent
{
	public Guid Id { get; init; }

	public string Name { get; init; }

	public string Surname { get; init; }

	public string? Patronymic { get; init; }

	public string BelongsToGroup { get; init; }

	public ulong Recordbook { get; init; }

	public AssignmentValue Assignment { get; init; }

	internal TeacherJournalStudent(StudentAssignment studentAssignment, string group)
	{
		Name = studentAssignment.Student.Name.Name;
		Surname = studentAssignment.Student.Name.Surname;
		Patronymic = studentAssignment.Student.Name.Patronymic;
		BelongsToGroup = group;
		Recordbook = studentAssignment.Student.Recordbook.Recordbook;
		Assignment = studentAssignment.Value;
		Id = studentAssignment.Id;
	}
}
