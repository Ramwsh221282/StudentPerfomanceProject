using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments.Errors;

public sealed class DepartmentNameLengthError : Error
{
	public DepartmentNameLengthError(int maxLength) => error = $"Название кафедры превышает длину: {maxLength} символов";
	public override string ToString() => error;
}
