using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Teachers.Errors;

public sealed class TeacherMaxNameLengthError : Error
{
	public TeacherMaxNameLengthError(int maxLength) => error = $"Имя преподавателя превышает {maxLength} символов";
	public override string ToString() => error;
}
