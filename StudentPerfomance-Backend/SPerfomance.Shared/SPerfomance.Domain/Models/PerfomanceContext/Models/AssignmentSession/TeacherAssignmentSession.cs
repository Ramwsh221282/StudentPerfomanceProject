using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks;
using SPerfomance.Domain.Models.PerfomanceContext.Models.TeacherJournals;
using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Models.Teachers.ValueObjects;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;

public class TeacherAssignmentSession
{
	private List<TeacherJournal> _journals = [];

	public TeacherName Teacher { get; init; }

	public TeacherAssignmentSession(Teacher teacher, AssignmentSession session)
	{
		Teacher = teacher.Name;
		foreach (AssignmentWeek week in session.Weeks)
		{
			TeacherJournal journal = new TeacherJournal();
			journal = journal.SetGroupName(week.Group!);
			Assignment[] assignments = week.Assignments.Where(a => a.Assigner == teacher.Name).ToArray();
			var groupedByDiscipline = assignments.GroupBy(a => a.Discipline);
			foreach (var disciplines in groupedByDiscipline)
			{
				TeacherJournalDiscipline discipline = new TeacherJournalDiscipline(disciplines.Key);
				foreach (var student in disciplines)
				{
					discipline.AppendStudent(student);
				}
				journal.SetDiscipline(discipline);
			}
			_journals.Add(journal);
		}
	}

	public List<TeacherJournal> Journals => _journals;
}
