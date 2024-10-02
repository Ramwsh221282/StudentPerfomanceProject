using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Teachers.Errors;

public sealed class TeacherNotFoundError : Error
{
	public TeacherNotFoundError() => error = "Преподаватель не был найден";
	public override string ToString() => error;
}
