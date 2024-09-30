using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Students.Errors;

public sealed class StudentThirdnameLengthError : Error
{
	public StudentThirdnameLengthError(int maxLength) => error = $"Отчество студента превышает {maxLength} символов";
	public override string ToString() => error;
}
