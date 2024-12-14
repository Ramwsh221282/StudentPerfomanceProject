using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services.AssignmentSessionViewServices.Views;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.Abstractions;

public interface IControlWeekReportRepository
{
    Task<Result<AssignmentSessionView>> Insert(
        AssignmentSessionView view,
        CancellationToken ct = default
    );
    Task<int> Count(CancellationToken ct = default);
    Task<bool> Remove(string id, CancellationToken ct = default);
    Task<bool> CanCreateControlWeek(
        DateTime startDate,
        string type,
        byte number,
        CancellationToken ct = default
    );
}
