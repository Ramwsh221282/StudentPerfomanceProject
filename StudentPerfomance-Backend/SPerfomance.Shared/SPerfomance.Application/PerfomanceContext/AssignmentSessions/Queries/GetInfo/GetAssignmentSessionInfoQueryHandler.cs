using SPerfomance.Application.Abstractions;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.DTO;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.Queries.GetInfo;

public sealed class GetAssignmentSessionInfoQueryHandler(IAssignmentSessionsRepository repository)
    : IQueryHandler<GetAssignmentSessionInfoQuery, AssignmentSessionInfoDTO>
{
    public async Task<Result<AssignmentSessionInfoDTO>> Handle(
        GetAssignmentSessionInfoQuery command,
        CancellationToken ct = default
    )
    {
        AssignmentSession? session = await repository.GetActiveSession(ct);
        if (session == null)
            return AssignmentSessionErrors.NoActiveFound();

        var dateStart = session.SessionStartDate.ToString("yyyy.MM.dd");
        var dateEnd = session.SessionCloseDate.ToString("yyyy.MM.dd");
        var daysToEnd = CalculateDaysUntilEnd(session);
        var completionPercent = CalculateAssignmentsCompletionPercent(session);
        return new AssignmentSessionInfoDTO(dateStart, dateEnd, daysToEnd, completionPercent);
    }

    private static int CalculateDaysUntilEnd(AssignmentSession session)
    {
        var currentDate = DateTime.Now;
        var difference = session.SessionCloseDate - currentDate;
        return difference.Days + 1;
    }

    private static int CalculateAssignmentsCompletionPercent(AssignmentSession session)
    {
        return 1;
    }
}
