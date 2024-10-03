using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Students.Errors;

public sealed class StudentNotFoundError : Error
{
	public StudentNotFoundError() => error = "Студент не найден";
}
