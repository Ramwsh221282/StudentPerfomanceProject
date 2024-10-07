using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Teachers.Errors;

public sealed class TeacherNullError : Error
{
	public TeacherNullError() => error = "Объект преподавателя был пустым";
	public override string ToString() => error;
}
