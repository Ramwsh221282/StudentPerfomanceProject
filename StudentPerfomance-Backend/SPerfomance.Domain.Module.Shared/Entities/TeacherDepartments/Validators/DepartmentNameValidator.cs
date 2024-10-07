using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments.Validators;

internal sealed class DepartmentNameValidator(string name) : Validator<string>
{
	private int _maxLength = 100;
	private readonly string _name = name;
	public override bool Validate()
	{
		if (string.IsNullOrWhiteSpace(_name))
			error.AppendError(new DepartmentNameError());
		if (!string.IsNullOrWhiteSpace(_name) && _name.Length > _maxLength)
			error.AppendError(new DepartmentNameLengthError(_maxLength));
		return HasError;
	}
}
