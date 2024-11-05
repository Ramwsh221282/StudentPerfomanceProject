using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.StudentGroups.ValueObjects;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.TeacherJournals;

public class TeacherJournal
{
	private List<TeacherJournalDiscipline> _disciplines = [];

	public StudentGroupName? GroupName { get; private set; }

	public IReadOnlyCollection<TeacherJournalDiscipline> Disciplines => _disciplines;

	internal TeacherJournal SetGroupName(StudentGroup group)
	{
		GroupName = group.Name;
		return this;
	}

	internal TeacherJournal SetDiscipline(TeacherJournalDiscipline discipline)
	{
		_disciplines.Add(discipline);
		return this;
	}
}
