using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments.Errors;

public sealed class DepartmentNameError : Error
{
	public DepartmentNameError() => error = "Название кафедры было пустым";
	public override string ToString() => error;
}
