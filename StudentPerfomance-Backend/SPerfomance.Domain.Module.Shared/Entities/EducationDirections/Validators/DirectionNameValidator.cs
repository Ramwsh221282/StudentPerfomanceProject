using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections.Errors;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections.ValueObjects;

namespace SPerfomance.Domain.Module.Shared.Entities.EducationDirections.Validators;

internal sealed class DirectionNameValidator(DirectionName name) : Validator<DirectionName>
{
	private const int MAX_NAME_LENGTH = 100;
	private readonly DirectionName _name = name;
	public override bool Validate()
	{
		if (_name == null)
			error.AppendError(new EducationDirectionNameError());
		if (_name != null && string.IsNullOrWhiteSpace(_name.Name))
			error.AppendError(new EducationDirectionNameError());
		if (_name != null && !string.IsNullOrWhiteSpace(_name.Name) && _name.Name.Length > MAX_NAME_LENGTH)
			error.AppendError(new EducationDirectionNameLengthError(MAX_NAME_LENGTH));
		return HasError;
	}
}
