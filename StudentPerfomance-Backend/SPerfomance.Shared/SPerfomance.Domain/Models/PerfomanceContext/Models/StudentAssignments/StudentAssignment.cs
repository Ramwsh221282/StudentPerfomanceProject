using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments;
using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments.ValueObjects;
using SPerfomance.Domain.Models.Students;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;

public class StudentAssignment : DomainEntity
{
	public Assignment Assignment { get; init; }

	public AssignmentValue Value { get; private set; }

	public Student Student { get; init; }

	internal StudentAssignment() : base(Guid.Empty)
	{
		Assignment = new Assignment();
		Value = AssignmentValue.Empty;
		Student = Student.Empty;
	}

	internal StudentAssignment(Assignment assignment, Student student) : base(Guid.NewGuid())
	{
		Assignment = assignment;
		Value = AssignmentValue.Empty;
		Student = student;
	}

	public Result<StudentAssignment> Assign(int value)
	{
		Result<AssignmentValue> assignment = AssignmentValue.Create((byte)value);
		if (assignment.IsFailure)
			return assignment.Error;

		Value = assignment.Value;
		return this;
	}
}
