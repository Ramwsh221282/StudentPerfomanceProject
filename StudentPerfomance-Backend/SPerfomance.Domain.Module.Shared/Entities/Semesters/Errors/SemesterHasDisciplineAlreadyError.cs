using System.Text;

using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Semesters.Errors;

public class SemesterHasDisciplineAlreadyError : Error
{
	public SemesterHasDisciplineAlreadyError(Semester semester)
	{
		StringBuilder messageBuilder = new StringBuilder();
		messageBuilder.AppendLine($"Семестр: {semester.Number.Value}");
		messageBuilder.AppendLine($"Года набора: {semester.Plan.Year.Year}");
		messageBuilder.AppendLine($"Направления подготовки:");
		messageBuilder.AppendLine($"{semester.Plan.Direction.Name.Name}");
		messageBuilder.AppendLine($"{semester.Plan.Direction.Code.Code}");
		messageBuilder.AppendLine($"{semester.Plan.Direction.Type.Type}");
		messageBuilder.AppendLine("Уже содержит данную дисциплину");
		error = messageBuilder.ToString();
	}
	public override string ToString() => error;
}
