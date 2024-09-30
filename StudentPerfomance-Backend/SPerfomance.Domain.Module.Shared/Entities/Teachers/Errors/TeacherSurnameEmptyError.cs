using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Teachers.Errors;

public sealed class TeacherSurnameEmptyError : Error
{
	public TeacherSurnameEmptyError() => error = "Фамилия преподавателя была пустая";
	public override string ToString() => error;
}
