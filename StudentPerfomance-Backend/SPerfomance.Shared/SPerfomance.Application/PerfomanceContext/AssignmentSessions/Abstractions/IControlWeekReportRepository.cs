using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services.AssignmentSessionViewServices.Views;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.Abstractions;

public interface IControlWeekReportRepository
{
    Task<Result<AssignmentSessionView>> Insert(AssignmentSessionView view);
}
