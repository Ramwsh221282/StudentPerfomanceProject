using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.DTO;

public class AssignmentSessionDTO
{
	public int Number { get; init; }

	public string StartDate { get; init; }

	public string? EndDate { get; init; }

	public string State { get; init; }

	public List<AssignmentWeekDTO> Weeks { get; init; }

	public AssignmentSessionDTO(AssignmentSession session)
	{
		Number = session.EntityNumber;
		StartDate = session.SessionStartDate.ToString("yyyy-MM-dd");
		EndDate = session.SessionCloseDate.ToString("yyyy-MM-dd");
		State = session.State.State == true ? "Открыта" : "Закрыта";
		Weeks = session.Weeks.Select(s => new AssignmentWeekDTO(s)).ToList();
	}
}
