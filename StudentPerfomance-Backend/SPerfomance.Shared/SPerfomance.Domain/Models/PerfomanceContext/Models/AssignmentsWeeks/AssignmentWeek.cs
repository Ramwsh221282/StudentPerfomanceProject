using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments;
using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments.Errors;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;
using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.Students;
using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks;

public class AssignmentWeek : DomainEntity
{
	private readonly List<Assignment> _assignments = [];

	public AssignmentSession Session { get; private set; }

	public StudentGroup? Group { get; private set; }

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

		foreach (Student student in group.Students)
		{
			foreach (SemesterPlan discipline in disciplines)
			{
				Assignment assignment = new Assignment(this, discipline, student);
				_assignments.Add(assignment);
			}
		}
	}

	internal Result<AssignmentWeek> MakeAssignment(Teacher teacher, SemesterPlan semesterPlan, Student student, byte value)
	{
		Assignment? assignment = _assignments.FirstOrDefault
		(
			a =>
			a.AssignedTo == student.Name &&
			a.AssignedToRecordBook == student.Recordbook &&
			a.AssignetToGroup == student.AttachedGroup.Name &&
			a.Discipline == semesterPlan.Discipline
		);
		if (assignment == null)
			return AssignmentErrors.NotExist(student, semesterPlan);

		Result<Assignment> mark = assignment.Assign(value, teacher);
		if (mark.IsFailure)
			return mark.Error;

		assignment = mark.Value;
		return this;
	}
}
