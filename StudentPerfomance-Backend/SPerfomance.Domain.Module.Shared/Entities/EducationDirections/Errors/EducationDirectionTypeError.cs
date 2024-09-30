using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.EducationDirections.Errors;

public sealed class EducationDirectionTypeError : Error
{
	public EducationDirectionTypeError() => error = "Тип направления некорректен";
	public override string ToString() => error;
}
