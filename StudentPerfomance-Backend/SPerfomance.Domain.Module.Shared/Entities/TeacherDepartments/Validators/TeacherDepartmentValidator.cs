using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments.Validators;

internal sealed class TeacherDepartmentValidator(TeachersDepartment department) : Validator<TeachersDepartment>
{
	private readonly TeachersDepartment _department = department;
	private readonly int _maxNameLength = 100;
	public override bool Validate()
	{
		if (string.IsNullOrWhiteSpace(_department.FullName))
			error.AppendError(new DepartmentNameError());
		if (!string.IsNullOrWhiteSpace(_department.FullName) && _department.FullName.Length > _maxNameLength)
			error.AppendError(new DepartmentNameLengthError(_maxNameLength));
		return HasError;
	}
}
