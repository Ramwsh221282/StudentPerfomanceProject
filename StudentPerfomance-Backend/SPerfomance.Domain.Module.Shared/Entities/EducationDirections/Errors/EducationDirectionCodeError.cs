using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.EducationDirections.Errors;

public class EducationDirectionCodeError : Error
{
	public EducationDirectionCodeError() => error = "Некорректный код направления";
	public override string ToString() => error;
}
