using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Teachers.Errors;

public sealed class TeacherMaxThirdnameLengthError : Error
{
	public TeacherMaxThirdnameLengthError(int maxLength) => error = $"Отчество преподавателя превышает {maxLength} символов";
	public override string ToString() => error;
}
