using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.Teachers.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.Teachers.ValueObjects;

public class TeacherName : DomainValueObject
{
	private const int MAX_NAMES_LENGTH = 40;

	public string Name { get; private set; }

	public string Surname { get; private set; }

	public string Patronymic { get; private set; }

	private TeacherName()
	{
		Name = string.Empty;
		Surname = string.Empty;
		Patronymic = string.Empty;
	}

	private TeacherName(
		string name,
		string surname,
		string patronymic
	)
	{
		Name = name;
		Surname = surname;
		Patronymic = patronymic;
	}

	internal static TeacherName Empty => new TeacherName();

	internal static Result<TeacherName> Create(string name, string surname, string? patronymic)
	{
		if (string.IsNullOrWhiteSpace(name))
			return Result<TeacherName>.Failure(TeacherErrors.NameEmpty());

		if (string.IsNullOrWhiteSpace(surname))
			return Result<TeacherName>.Failure(TeacherErrors.SurnameEmpty());

		if (name.Length > MAX_NAMES_LENGTH)
			return Result<TeacherName>.Failure(TeacherErrors.NameExceess(MAX_NAMES_LENGTH));

		if (surname.Length > MAX_NAMES_LENGTH)
			return Result<TeacherName>.Failure(TeacherErrors.SurnameExceess(MAX_NAMES_LENGTH));

		if (!string.IsNullOrWhiteSpace(patronymic) && patronymic.Length > MAX_NAMES_LENGTH)
			return Result<TeacherName>.Failure(TeacherErrors.PatronymicExceess(MAX_NAMES_LENGTH));

		return Result<TeacherName>.Success(new TeacherName(name, surname, patronymic.ValueOrEmpty()));
	}

	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return Name;
		yield return Surname;
		yield return Patronymic;
	}
}
