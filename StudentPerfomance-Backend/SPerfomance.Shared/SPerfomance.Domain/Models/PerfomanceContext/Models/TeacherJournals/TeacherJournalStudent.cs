using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments.ValueObjects;
using SPerfomance.Domain.Models.Students.ValueObjects;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.TeacherJournals;

public class TeacherJournalStudent
{
	public string Name { get; init; }

	public string Surname { get; init; }

	public string? Patronymic { get; init; }

	public AssignmentValue Assignment { get; init; }

	internal TeacherJournalStudent(StudentName name, AssignmentValue? assignment)
	{
		Name = name.Name;
		Surname = name.Surname;
		Patronymic = name.Patronymic;
		Assignment = assignment == null ? AssignmentValue.Empty : assignment;
	}
}
