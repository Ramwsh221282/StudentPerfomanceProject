using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Students.Errors;

public sealed class StudentSurnameError : Error
{
	public StudentSurnameError() => error = "Фамилия студента была пустой";
	public override string ToString() => error;
}
