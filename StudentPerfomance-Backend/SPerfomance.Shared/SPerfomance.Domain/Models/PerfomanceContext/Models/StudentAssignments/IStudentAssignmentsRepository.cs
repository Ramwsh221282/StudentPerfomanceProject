namespace SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;

public interface IStudentAssignmentsRepository
{
	Task<StudentAssignment?> ReceiveAssignment(Guid id);

	Task UpdateAssignmentValue(StudentAssignment assignment);
}
