using System.Text;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.EducationDirections.Errors;

public sealed class DirectionSameNameError : Error
{
	public DirectionSameNameError(EducationDirection direction)
	{
		StringBuilder messageBuilder = new StringBuilder();
		messageBuilder.AppendLine($"Направление подготовки: {direction.Code} {direction.Type.Type}");
		messageBuilder.AppendLine($"Уже имеет такое название");
		messageBuilder.AppendLine($"Операция отменена.");
	}
	public override string ToString() => error.ToString();
}
