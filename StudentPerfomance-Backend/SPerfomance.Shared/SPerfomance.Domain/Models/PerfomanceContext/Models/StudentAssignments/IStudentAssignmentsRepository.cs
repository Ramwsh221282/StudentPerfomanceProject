namespace SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;

public interface IStudentAssignmentsRepository
{
    Task<StudentAssignment?> ReceiveAssignment(Guid id, CancellationToken ct = default);

    Task UpdateAssignmentValue(StudentAssignment assignment, CancellationToken ct = default);
}
