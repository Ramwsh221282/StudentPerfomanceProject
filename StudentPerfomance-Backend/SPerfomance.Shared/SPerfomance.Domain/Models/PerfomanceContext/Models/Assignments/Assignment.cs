using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments.ValueObjects;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks;
using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.SemesterPlans.ValueObjects;
using SPerfomance.Domain.Models.StudentGroups.ValueObjects;
using SPerfomance.Domain.Models.Students;
using SPerfomance.Domain.Models.Students.ValueObjects;
using SPerfomance.Domain.Models.TeacherDepartments.ValueObjects;
using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Models.Teachers.ValueObjects;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments;

public class Assignment : DomainEntity
{
	public AssignmentWeek Week { get; private set; }

	public DateTime AssignmentOpenDate { get; private set; }

	public DateTime? AssignmentCloseDate { get; private set; }

	public DisciplineName Discipline { get; private set; }

	public StudentName AssignedTo { get; private set; }

	public StudentRecordbook AssignedToRecordBook { get; private set; }

	public StudentGroupName AssignetToGroup { get; private set; }

	public AssignmentState State { get; private set; }

	public TeacherName? Assigner { get; private set; }

	public DepartmentName? AssignerDepartment { get; private set; }

	public AssignmentValue? Value { get; private set; }

	private Assignment() : base(Guid.Empty)
	{
		AssignmentOpenDate = DateTime.MinValue;
		Assigner = TeacherName.Empty;
		AssignerDepartment = DepartmentName.Empty;
		Discipline = DisciplineName.Empty;
		AssignedTo = StudentName.Empty;
		AssignetToGroup = StudentGroupName.Empty;
		Value = AssignmentValue.Empty;
		Week = AssignmentWeek.Empty;
		State = AssignmentState.Locked;
		AssignedToRecordBook = StudentRecordbook.Empty;
	}

	internal Assignment(
		AssignmentWeek week,
		SemesterPlan discipline,
		Student assignedTo
	) : base(Guid.NewGuid())
	{
		Week = week;
		Assigner = TeacherName.Empty;
		AssignerDepartment = DepartmentName.Empty;
		Discipline = discipline.Discipline;
		AssignedTo = assignedTo.Name;
		AssignedToRecordBook = assignedTo.Recordbook;
		AssignetToGroup = assignedTo.AttachedGroup.Name;
		Value = AssignmentValue.NotAttestated;
		AssignmentOpenDate = DateTime.Now;
		State = AssignmentState.Opened;
	}

	internal Result<Assignment> Assign(byte value, Teacher teacher)
	{
		Result<AssignmentValue> mark = AssignmentValue.Create(value);
		if (mark.IsFailure)
			return mark.Error;

		Value = mark.Value;
		Assigner = teacher.Name;
		AssignerDepartment = teacher.Department.Name;
		AssignmentCloseDate = DateTime.Now;
		State = AssignmentState.Locked;
		return this;
	}
}
