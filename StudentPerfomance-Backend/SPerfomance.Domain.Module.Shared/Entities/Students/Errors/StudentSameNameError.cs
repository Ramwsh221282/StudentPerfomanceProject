using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Students.Errors;

public sealed class StudentSameNameError : Error
{
	public StudentSameNameError() => error = "Студент уже имеет такое имя";
	public override string ToString() => error;
}
