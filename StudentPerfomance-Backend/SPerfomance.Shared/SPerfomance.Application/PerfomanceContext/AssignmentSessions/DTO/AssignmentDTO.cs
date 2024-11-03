using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.DTO;

public class AssignmentDTO
{
	public string AssignerName { get; init; }

	public string AssignerSurname { get; init; }

	public string? AssignerPatronymic { get; init; }

	public string AssignerDepartment { get; init; }

	public string AssignedToName { get; init; }

	public string AssignedToSurname { get; init; }

	public string? AssignedToPatronymic { get; init; }

	public ulong AssignedToRecordbook { get; init; }

	public string? AssignmentValue { get; init; }

	public string? AssignmentDate { get; init; }

	public string Discipline { get; init; }


	public AssignmentDTO(Assignment assignment)
	{
		AssignerName = assignment.Assigner.Name;
		AssignerSurname = assignment.Assigner.Surname;
		AssignerPatronymic = assignment.Assigner.Patronymic;
		AssignerDepartment = assignment.AssignerDepartment.Name;
		AssignedToName = assignment.AssignedTo.Name;
		AssignedToSurname = assignment.AssignedTo.Surname;
		AssignedToPatronymic = assignment.AssignedTo.Patronymic;
		AssignedToRecordbook = assignment.AssignedToRecordBook.Recordbook;
		AssignmentValue = MapAssignmentValue(assignment);
		AssignmentDate = assignment.AssignmentCloseDate == null ? "Не проставлена" : assignment.AssignmentCloseDate.Value.ToString("yyyy-MM-dd");
		Discipline = assignment.Discipline.Name;
	}

	private string MapAssignmentValue(Assignment assignment)
	{
		if (assignment.Value == null)
			return "Нет проставителя";

		return assignment.Value.Value switch
		{
			0 => "Не аттестован",
			1 => "Нет проставления",
			2 => "2",
			3 => "3",
			4 => "4",
			5 => "5",
			_ => "Нет проставления"
		};
	}
}
