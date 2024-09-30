using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Teachers.Errors;

public sealed class TeacherNameEmptyError : Error
{
	public TeacherNameEmptyError() => error = "Имя преподавателя пустое";
	public override string ToString() => error;
}
