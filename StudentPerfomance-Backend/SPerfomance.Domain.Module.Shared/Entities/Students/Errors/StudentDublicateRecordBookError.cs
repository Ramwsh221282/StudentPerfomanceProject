using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Students.Errors;

public sealed class StudentDublicateRecordBookError : Error
{
	public StudentDublicateRecordBookError(ulong recordBook) => error = $"Зачётная книжка {recordBook} занята";
	public override string ToString() => error;
}
