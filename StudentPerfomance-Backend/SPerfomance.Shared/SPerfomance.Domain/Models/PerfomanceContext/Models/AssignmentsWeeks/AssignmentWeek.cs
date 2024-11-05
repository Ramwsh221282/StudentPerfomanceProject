using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;
using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.StudentGroups;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks;

public class AssignmentWeek : DomainEntity
{
	private readonly List<Assignment> _assignments = [];

	public AssignmentSession Session { get; private init; }

	public StudentGroup Group { get; private init; }

	internal AssignmentWeek() : base(Guid.Empty)
	{
		Group = StudentGroup.Empty;
		Session = AssignmentSession.Empty;
	}

	internal AssignmentWeek(StudentGroup group, AssignmentSession session) : base(Guid.NewGuid())
	{
		Group = group;
		Session = session;
		FillEmptyAssignments(group);
	}

	public IReadOnlyCollection<Assignment> Assignments => _assignments;

	internal static AssignmentWeek Empty => new AssignmentWeek();

	private void FillEmptyAssignments(StudentGroup group)
	{
		IReadOnlyCollection<SemesterPlan> disciplines = group.ActiveGroupSemester!.Disciplines;

		foreach (var discipline in disciplines)
		{
			Assignment assignment = new Assignment(discipline, this, group);
			_assignments.Add(assignment);
		}
	}
}
