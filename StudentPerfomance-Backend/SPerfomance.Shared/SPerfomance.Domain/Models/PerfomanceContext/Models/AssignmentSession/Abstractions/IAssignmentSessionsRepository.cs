using SPerfomance.Domain.Models.Teachers;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession.Abstractions;

public interface IAssignmentSessionsRepository
{
    Task Insert(AssignmentSession session, CancellationToken ct = default);
    Task Remove(AssignmentSession session, CancellationToken ct = default);
    Task<int> GenerateEntityNumber(CancellationToken ct = default);
    Task<int> Count(CancellationToken ct = default);
    Task<AssignmentSession?> GetActiveSession(CancellationToken ct = default);
    Task<TeacherAssignmentSession?> GetAssignmentSessionForTeacher(
        Teacher teacher,
        CancellationToken ct = default
    );
    Task<AssignmentSession?> GetById(Guid id, CancellationToken ct = default);
    Task<bool> HasAnyActive(CancellationToken ct = default);
}
