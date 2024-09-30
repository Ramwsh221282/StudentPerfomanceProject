using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections.Errors;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections.ValueObjects;

namespace SPerfomance.Domain.Module.Shared.Entities.EducationDirections.Validators;

internal sealed class DirectionTypeValidator(DirectionType type) : Validator<DirectionType>
{
	private readonly DirectionType _type = type;
	private readonly DirectionType[] _types = [new BachelorDirection(), new MagisterDirection()];
	public override bool Validate()
	{
		if (_type == null)
			error.AppendError(new EducationDirectionTypeError());
		if (_type != null && _types.Any(t => t.Type == _type.Type) == false)
			error.AppendError(new EducationDirectionTypeError());
		return HasError;
	}
}
