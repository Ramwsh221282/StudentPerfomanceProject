using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Entities.Teachers.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Teachers.ValueObjects;

namespace SPerfomance.Domain.Module.Shared.Entities.Teachers.Validators;

internal sealed class TeacherNameValidator : Validator<TeacherName>
{
	private const int MAX_NAMES_LENGTH = 50;
	private readonly string _name;
	private readonly string _surname;
	private readonly string? _thirdname;

	public TeacherNameValidator(string name, string surname, string? thirdname) =>
		(_name, _surname, _thirdname) = (name, surname, thirdname);

	public override bool Validate()
	{
		if (string.IsNullOrWhiteSpace(_name))
			error.AppendError(new TeacherNameEmptyError());
		if (string.IsNullOrWhiteSpace(_surname))
			error.AppendError(new TeacherSurnameEmptyError());
		if (!string.IsNullOrWhiteSpace(_name) && _name.Length > MAX_NAMES_LENGTH)
			error.AppendError(new TeacherMaxNameLengthError(MAX_NAMES_LENGTH));
		if (!string.IsNullOrWhiteSpace(_surname) && _surname.Length > MAX_NAMES_LENGTH)
			error.AppendError(new TeacherMaxSurnameLengthError(MAX_NAMES_LENGTH));
		if (!string.IsNullOrWhiteSpace(_thirdname) && _thirdname.Length > MAX_NAMES_LENGTH)
			error.AppendError(new TeacherMaxThirdnameLengthError(MAX_NAMES_LENGTH));
		return HasError;
	}
}
