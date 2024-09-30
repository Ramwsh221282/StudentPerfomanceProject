using StudentPerfomance.Domain.Errors.EducationDirections;
using StudentPerfomance.Domain.ValueObjects.EducationDirections;

namespace StudentPerfomance.Domain.Validators.EducationDIrections;

// Валидатор объекта значения Direction Name.
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
		return error.ToString().Length == 0;
	}
}
