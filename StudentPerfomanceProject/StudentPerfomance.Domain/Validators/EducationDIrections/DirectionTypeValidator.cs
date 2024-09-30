using StudentPerfomance.Domain.Errors.EducationDirections;
using StudentPerfomance.Domain.ValueObjects.EducationDirections;

namespace StudentPerfomance.Domain.Validators.EducationDIrections;

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
		return error.ToString().Length == 0;
	}
}
