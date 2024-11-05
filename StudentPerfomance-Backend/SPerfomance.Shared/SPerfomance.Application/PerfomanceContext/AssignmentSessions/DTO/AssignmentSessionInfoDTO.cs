namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.DTO;

public class AssignmentSessionInfoDTO
{
	public string DateStart { get; init; }

	public string DateEnd { get; init; }

	public int DaysToEnd { get; init; }

	public int CompletedPercent { get; init; }

	public AssignmentSessionInfoDTO(string dateStart, string dateEnd, int daysToEnd, int completedPercent)
	{
		DateStart = dateStart;
		DateEnd = dateEnd;
		DaysToEnd = daysToEnd;
		CompletedPercent = completedPercent;
	}
}
