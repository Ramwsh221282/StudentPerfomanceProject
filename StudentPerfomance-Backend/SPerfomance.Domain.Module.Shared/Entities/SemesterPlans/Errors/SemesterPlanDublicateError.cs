using System.Text;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.SemesterPlans.Errors;

public sealed class SemesterPlanDublicateError : Error
{
	public SemesterPlanDublicateError(byte semesterNumber, uint yearOfCreation, string directionName, string disciplineName)
	{
		StringBuilder messageBuilder = new StringBuilder();
		messageBuilder.AppendLine($"Для семестра: {semesterNumber}");
		messageBuilder.AppendLine($"Года набора: {yearOfCreation}");
		messageBuilder.AppendLine($"Направления: {directionName}");
		messageBuilder.AppendLine($"Невозможно добавить дисциплину: {disciplineName}");
		messageBuilder.AppendLine($"Так как в семестр эта дисциплина уже входит");
		error = messageBuilder.ToString();
	}

	public override string ToString() => error;
}
