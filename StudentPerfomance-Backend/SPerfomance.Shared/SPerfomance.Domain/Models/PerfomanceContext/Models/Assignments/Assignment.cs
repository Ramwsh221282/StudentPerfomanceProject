using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks;
using SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;
using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.Students;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments;

public class Assignment : DomainEntity
{
	public AssignmentWeek Week { get; init; }

	public SemesterPlan Discipline { get; init; }

	private List<StudentAssignment> _studentAssignments = [];

	public IReadOnlyCollection<StudentAssignment> StudentAssignments => _studentAssignments;

	internal Assignment() : base(Guid.Empty)
	{
		Discipline = SemesterPlan.Empty;
		Week = new AssignmentWeek();
	}

	internal Assignment(SemesterPlan discipline, AssignmentWeek week, StudentGroup group) : base(Guid.NewGuid())
	{
		Discipline = discipline;
		Week = week;
		FillEmptyStudentAssignments(group);
	}

	internal void FillEmptyStudentAssignments(StudentGroup group)
	{
		foreach (Student student in group.Students)
		{
			StudentAssignment studentAssignment = new StudentAssignment(this, student);
			_studentAssignments.Add(studentAssignment);
		}
	}
}
