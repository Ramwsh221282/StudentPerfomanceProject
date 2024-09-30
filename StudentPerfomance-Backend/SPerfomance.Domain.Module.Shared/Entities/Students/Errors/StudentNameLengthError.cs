using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Students.Errors;

public sealed class StudentNameLengthError : Error
{
	public StudentNameLengthError(int maxLength) => error = $"Имя студента превышает {maxLength} символов";
	public override string ToString() => error;
}
