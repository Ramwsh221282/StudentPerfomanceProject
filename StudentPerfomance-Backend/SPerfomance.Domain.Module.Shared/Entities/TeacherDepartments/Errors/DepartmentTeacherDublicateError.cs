using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments.Errors;

public sealed class DepartmentTeacherDublicateError : Error
{
	public DepartmentTeacherDublicateError() => error = "Кафедра уже имеет этого преподавателя";
	public override string ToString() => error;
}
