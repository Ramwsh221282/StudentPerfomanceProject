using System.Text;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.EducationDirections.Errors;

public sealed class DirectionCodeSameError : Error
{
	public DirectionCodeSameError(EducationDirection direction)
	{
		StringBuilder messageBuilder = new StringBuilder();
		messageBuilder.AppendLine($"Направление подготовки: {direction.Name} {direction.Type}");
		messageBuilder.AppendLine("Уже содержит данный код направления");
		error = messageBuilder.ToString();
	}
	public override string ToString() => error.ToString();
}
