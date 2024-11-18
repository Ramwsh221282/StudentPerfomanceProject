using SPerfomance.Domain.Models.Teachers;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Abstractions;

public interface IAssignmentSessionsRepository
{
    Task Insert(AssignmentSession session);

    Task Remove(AssignmentSession session);

    Task Update(AssignmentSession session);

    Task<IReadOnlyCollection<AssignmentSession>> GetPaged(int page, int pageSize);

    Task<IReadOnlyCollection<AssignmentSession>> GetInPeriodPaged(
        DateTime startDate,
        DateTime endDate,
        int page,
        int pageSize
    );

    Task GetByPeriod(DateTime startDate, DateTime endDate);

    Task<int> GenerateEntityNumber();

    Task<int> Count();

    Task<AssignmentSession?> GetActiveSession();

    Task<TeacherAssignmentSession?> GetAssignmentSessionForTeacher(Teacher teacher);

    Task<AssignmentSession?> GetById(Guid id);
}
