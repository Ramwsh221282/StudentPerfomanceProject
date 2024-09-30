using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.EducationDirections.Errors;

public sealed class EducationDirectionNameError : Error
{
	public EducationDirectionNameError() => error = "Некорректное название направления подготовки";
	public override string ToString() => error;
}
