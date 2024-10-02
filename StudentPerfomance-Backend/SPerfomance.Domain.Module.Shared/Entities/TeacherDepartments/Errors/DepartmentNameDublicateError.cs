using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments.Errors;

public sealed class DepartmentNameDublicateError : Error
{
	public DepartmentNameDublicateError(string name) => error = $"Кафедра с названием {name} уже существует";
	public override string ToString() => error;
}
