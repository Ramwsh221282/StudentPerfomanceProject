using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Errors;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.ValueObjects;

namespace SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Validators;

internal sealed class GroupNameValidator(GroupName name) : Validator<GroupName>
{
	private int _maxLength = 15;
	private readonly GroupName _name = name;
	public override bool Validate()
	{
		if (_name == null)
			error.AppendError(new GroupNameError());
		if (_name != null && string.IsNullOrWhiteSpace(_name.Name))
			error.AppendError(new GroupNameError());
		if (_name != null && !string.IsNullOrWhiteSpace(_name.Name) && _name.Name.Length > _maxLength)
			error.AppendError(new GroupNameLengthError(_maxLength));
		return HasError;
	}
}
