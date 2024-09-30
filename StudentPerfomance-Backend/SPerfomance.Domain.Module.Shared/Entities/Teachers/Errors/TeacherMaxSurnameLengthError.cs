using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Teachers.Errors;

public sealed class TeacherMaxSurnameLengthError : Error
{
	public TeacherMaxSurnameLengthError(int maxLength) => error = $"Фамилия преподавателя превышает {maxLength} символов";
	public override string ToString() => error;
}
