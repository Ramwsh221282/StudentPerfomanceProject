using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.DTO;

public class AssignmentWeekDTO
{
	public StudentGroupDTO? Group { get; init; }

	public List<AssignmentDTO> Assignments { get; init; } = [];

	public AssignmentWeekDTO(AssignmentWeek week)
	{
		Group = week.Group!.MapFromDomain();
		Assignments = week.Assignments.Select(a => new AssignmentDTO(a)).ToList();
	}
}
