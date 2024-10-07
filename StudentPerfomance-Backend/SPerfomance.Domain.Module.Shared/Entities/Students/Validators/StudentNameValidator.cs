using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Entities.Students.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Students.ValueObjects;

namespace SPerfomance.Domain.Module.Shared.Entities.Students.Validators;

internal sealed class StudentNameValidator(StudentName name) : Validator<StudentName>
{
	private readonly int _maxLength = 50;
	private readonly StudentName _name = name;

	public override bool Validate()
	{
		if (_name == null)
			error.AppendError(new StudentNameError());
		if (_name != null && string.IsNullOrWhiteSpace(_name.Name))
			error.AppendError(new StudentNameError());
		if (_name != null && string.IsNullOrWhiteSpace(_name.Surname))
			error.AppendError(new StudentSurnameError());
		if (_name != null && !string.IsNullOrWhiteSpace(_name.Name) && _name.Name.Length > _maxLength)
			error.AppendError(new StudentNameLengthError(_maxLength));
		if (_name != null && !string.IsNullOrWhiteSpace(_name.Surname) && _name.Surname.Length > _maxLength)
			error.AppendError(new StudentSurnameLengthError(_maxLength));
		if (_name != null && !string.IsNullOrWhiteSpace(_name.Thirdname) && _name.Thirdname.Length > _maxLength)
			error.AppendError(new StudentThirdnameLengthError(_maxLength));
		return HasError;
	}
}
