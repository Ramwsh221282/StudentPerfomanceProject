using StudentPerfomance.Domain.ValueObjects.Teacher;

namespace StudentPerfomance.Domain.Validators.Teacher;

internal class TeacherNameValidator : Validator<TeacherName>
{
	private const int MAX_NAMES_LENGTH = 50;
	private readonly string _name;
	private readonly string _surname;
	private readonly string? _thirdname;

	public TeacherNameValidator(string name, string surname, string? thirdname) =>
		(_name, _surname, _thirdname) = (name, surname, thirdname);

	public override bool Validate() =>
		ValidateName() && ValidateSurname() && ValidateThirdname();

	private bool ValidateName()
	{
		if (string.IsNullOrWhiteSpace(_name) || _name.Length > MAX_NAMES_LENGTH)
		{
			_errorBuilder.AppendLine("Имя пустое или длина выше 50 символов");
			return false;
		}
		return true;
	}

	private bool ValidateSurname()
	{
		if (string.IsNullOrWhiteSpace(_surname) || _surname.Length > MAX_NAMES_LENGTH)
		{
			_errorBuilder.AppendLine("Фамилия пустая или длина выше 50 символов");
			return false;
		}
		return true;
	}

	public bool ValidateThirdname()
	{
		if (!string.IsNullOrWhiteSpace(_thirdname) && _thirdname.Length > MAX_NAMES_LENGTH)
		{
			_errorBuilder.AppendLine("Длина отчества выше 50 символов");
			return false;
		}
		return true;
	}
}
