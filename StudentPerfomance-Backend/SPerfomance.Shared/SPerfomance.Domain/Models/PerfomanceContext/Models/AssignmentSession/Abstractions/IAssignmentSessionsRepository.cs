using SPerfomance.Domain.Models.Teachers;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Abstractions;

public interface IAssignmentSessionsRepository
{
    Task Insert(AssignmentSession session, CancellationToken ct = default);

    Task Remove(AssignmentSession session, CancellationToken ct = default);

    Task Update(AssignmentSession session, CancellationToken ct = default);

    Task<IReadOnlyCollection<AssignmentSession>> GetPaged(
        int page,
        int pageSize,
        CancellationToken ct = default
    );

    Task<IReadOnlyCollection<AssignmentSession>> GetInPeriodPaged(
        DateTime startDate,
        DateTime endDate,
        int page,
        int pageSize,
        CancellationToken ct = default
    );

    Task GetByPeriod(DateTime startDate, DateTime endDate, CancellationToken ct = default);

    Task<int> GenerateEntityNumber(CancellationToken ct = default);

    Task<int> Count(CancellationToken ct = default);

    Task<AssignmentSession?> GetActiveSession(CancellationToken ct = default);

    Task<TeacherAssignmentSession?> GetAssignmentSessionForTeacher(
        Teacher teacher,
        CancellationToken ct = default
    );

    Task<AssignmentSession?> GetById(Guid id, CancellationToken ct = default);
}
