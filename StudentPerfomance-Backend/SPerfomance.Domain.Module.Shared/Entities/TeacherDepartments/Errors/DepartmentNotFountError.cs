using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments.Errors;

public sealed class DepartmentNotFountError : Error
{
	public DepartmentNotFountError() => error = "Кафедра не найдена";
	public override string ToString() => error;
}
