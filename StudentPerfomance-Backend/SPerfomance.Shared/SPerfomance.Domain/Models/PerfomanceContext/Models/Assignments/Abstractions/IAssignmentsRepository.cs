using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments.Abstractions;

public interface IAssignmentsRepository
{
    Task<Assignment?> ReceiveAssignment(
        AssignmentSession session,
        string groupName,
        string studentName,
        string studentSurname,
        string? studentPatronymic,
        ulong studentRecordbook,
        string disciplineName
    );

    Task UpdateAssignmentMark(Assignment assignment);
}
