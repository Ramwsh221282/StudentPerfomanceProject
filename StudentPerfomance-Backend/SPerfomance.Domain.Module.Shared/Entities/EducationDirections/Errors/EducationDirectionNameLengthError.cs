using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.EducationDirections.Errors;

public class EducationDirectionNameLengthError : Error
{
	public EducationDirectionNameLengthError(int length) => error = $"Название направления подготовки превышает {length} символов";
	public override string ToString() => error;
}
