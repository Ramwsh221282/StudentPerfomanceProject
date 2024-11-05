using SPerfomance.Application.Abstractions;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.DTO;
using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments.ValueObjects;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.Queries.GetInfo;

public sealed class GetAssignmentSessionInfoQueryHandler
(
	IAssignmentSessionsRepository repository
)
: IQueryHandler<GetAssignmentSessionInfoQuery, AssignmentSessionInfoDTO>
{
	private readonly IAssignmentSessionsRepository _repository = repository;

	public async Task<Result<AssignmentSessionInfoDTO>> Handle(GetAssignmentSessionInfoQuery command)
	{
		AssignmentSession? session = await _repository.GetActiveSession();
		if (session == null)
			return AssignmentSessionErrors.NoActiveFound();

		string dateStart = session.SessionStartDate.ToString("yyyy.MM.dd");
		string dateEnd = session.SessionCloseDate.ToString("yyyy.MM.dd");
		int daysToEnd = CalculateDaysUntilEnd(session);
		int completionPercent = CalculateAssignmentsCompletionPercent(session);
		return new AssignmentSessionInfoDTO(dateStart, dateEnd, daysToEnd, completionPercent);
	}

	private int CalculateDaysUntilEnd(AssignmentSession session)
	{
		DateTime currentDate = DateTime.Now;
		TimeSpan difference = session.SessionCloseDate - currentDate;
		return (int)difference.Days + 1;
	}

	private int CalculateAssignmentsCompletionPercent(AssignmentSession session)
	{
		int totalAmount = 0;
		int completedCount = 0;
		foreach (var week in session.Weeks)
		{
			totalAmount += week.Assignments.Count;
			completedCount += week.Assignments.Count(a => a.State == AssignmentState.Locked);
		}

		if (completedCount == 0)
			return 0;

		double percentage = (double)completedCount / totalAmount * 100;
		return (int)Math.Round(percentage, 2);
	}
}
