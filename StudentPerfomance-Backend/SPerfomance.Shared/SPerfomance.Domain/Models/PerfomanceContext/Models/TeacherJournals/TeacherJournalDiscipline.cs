using SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;
using SPerfomance.Domain.Models.SemesterPlans.ValueObjects;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.TeacherJournals;

public sealed class TeacherJournalDiscipline
{
	private List<TeacherJournalStudent> _students = [];

	public DisciplineName Name { get; init; }

	public IReadOnlyCollection<TeacherJournalStudent> Students => _students;

	internal TeacherJournalDiscipline(DisciplineName name)
	{
		Name = name;
	}

	internal void AppendStudent(StudentAssignment studentAssignment, string group)
	{
		TeacherJournalStudent student = new TeacherJournalStudent(studentAssignment, group);
		_students.Add(student);
	}
}
