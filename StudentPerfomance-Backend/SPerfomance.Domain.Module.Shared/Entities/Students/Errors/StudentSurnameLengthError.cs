using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Students.Errors;

public sealed class StudentSurnameLengthError : Error
{
	public StudentSurnameLengthError(int maxLength) => error = $"Фамилия студента превышает {maxLength} символов";
	public override string ToString() => error;
}
