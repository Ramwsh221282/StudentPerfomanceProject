using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Validators;
using StudentPerfomance.Domain.Validators.EducationDIrections;

namespace StudentPerfomance.Domain.ValueObjects.EducationDirections;

internal sealed class EducationDirectionValidator(EducationDirection direction) : Validator<EducationDirection>
{
	private readonly EducationDirection _direction = direction;
	private readonly Validator<DirectionCode> _codeValidator = new DirectionCodeValidator(direction.Code);
	private readonly Validator<DirectionName> _nameValidator = new DirectionNameValidator(direction.Name);
	private readonly Validator<DirectionType> _typeValidator = new DirectionTypeValidator(direction.Type);
	public override bool Validate()
	{
		if (_direction == null) _errorBuilder.AppendLine("Направление подготовки некорректно");
		if (!_codeValidator.Validate()) _errorBuilder.AppendLine(_codeValidator.GetErrorText());
		if (!_nameValidator.Validate()) _errorBuilder.AppendLine(_nameValidator.GetErrorText());
		if (!_typeValidator.Validate()) _errorBuilder.AppendLine(_typeValidator.GetErrorText());
		return _errorBuilder.Length == 0;
	}
}
