using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments;
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

	internal void AppendStudent(Assignment assignment)
	{
		TeacherJournalStudent student = new TeacherJournalStudent(assignment.AssignedTo, assignment.Value);
		_students.Add(student);
	}
}
