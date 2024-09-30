using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Disciplines.Errors;

public sealed class DisciplineNameLengthError : Error
{
	public DisciplineNameLengthError(int maxLength) => error = $"Название дисциплины превышает {maxLength} символов";
	public override string ToString() => error;
}
