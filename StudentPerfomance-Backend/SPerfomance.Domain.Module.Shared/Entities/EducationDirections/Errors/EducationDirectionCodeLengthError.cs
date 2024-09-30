using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.EducationDirections.Errors;

public sealed class EducationDirectionCodeLengthError : Error
{
	public EducationDirectionCodeLengthError(int length) => error = $"Недопустимая длина кода направления. Максимально {length} символов";
	public override string ToString() => error;
}
