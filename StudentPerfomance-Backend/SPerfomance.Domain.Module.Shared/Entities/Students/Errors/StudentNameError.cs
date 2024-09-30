using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Students.Errors;

public sealed class StudentNameError : Error
{
	public StudentNameError() => error = "Имя студента было пустое";
	public override string ToString() => error;
}
