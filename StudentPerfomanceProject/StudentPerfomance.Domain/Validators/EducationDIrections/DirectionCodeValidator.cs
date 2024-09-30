using StudentPerfomance.Domain.Errors.EducationDirections;
using StudentPerfomance.Domain.ValueObjects.EducationDirections;

namespace StudentPerfomance.Domain.Validators.EducationDIrections;

internal sealed class DirectionCodeValidator(DirectionCode code) : Validator<DirectionCode>
{
	private readonly DirectionCode _code = code;
	private const int MAX_LENGTH_SIZE = 20;
	public override bool Validate()
	{
		if (_code == null)
			error.AppendError(new EducationDirectionCodeError());
		if (_code != null && string.IsNullOrWhiteSpace(_code.Code))
			error.AppendError(new EducationDirectionCodeError());
		if (_code != null && !string.IsNullOrEmpty(_code.Code) && _code.Code.Length > MAX_LENGTH_SIZE)
			error.AppendError(new EducationDirectionCodeLengthError(MAX_LENGTH_SIZE));
		return error.ToString().Length == 0;
	}
}
