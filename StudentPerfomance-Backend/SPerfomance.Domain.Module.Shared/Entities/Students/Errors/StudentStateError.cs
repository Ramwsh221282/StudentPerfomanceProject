using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Students.Errors;

public sealed class StudentStateError : Error
{
	public StudentStateError() => error = "Состояние студента либо \"Активен\" либо \"Неактивен\"";
	public override string ToString() => error;
}
