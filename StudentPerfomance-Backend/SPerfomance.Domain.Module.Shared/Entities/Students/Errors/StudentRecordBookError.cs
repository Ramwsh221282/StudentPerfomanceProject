using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Students.Errors;

public sealed class StudentRecordBookError : Error
{
	public StudentRecordBookError() => error = "Зачётная книжка это положительное число больше нуля";
	public override string ToString() => error;
}
