using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.DTO;

public class AssignmentDTO
{
	public string Discipline { get; init; }

	public List<StudentAssignmentDTO> StudentAssignments { get; init; }


	public AssignmentDTO(Assignment assignment)
	{
		Discipline = assignment.Discipline.Discipline.Name;
		StudentAssignments = assignment.StudentAssignments.Select(a => new StudentAssignmentDTO(a)).ToList();
	}
}
