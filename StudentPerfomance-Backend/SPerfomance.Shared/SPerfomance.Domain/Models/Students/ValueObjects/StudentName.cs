using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.Students.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.Students.ValueObjects;

public class StudentName : DomainValueObject
{
	public const int MAX_NAMES_LENGTH = 40;
	public string Name { get; private set; }
	public string Surname { get; private set; }
	public string Patronymic { get; private set; }

	private StudentName()
	{
		Name = string.Empty;
		Surname = string.Empty;
		Patronymic = string.Empty;
	}

	private StudentName(string name, string surname, string thirdname)
	{
		Name = name;
		Surname = surname;
		Patronymic = thirdname;
	}

	internal static StudentName Empty => new StudentName();

	internal static Result<StudentName> Create(string name, string surname, string? patronymic)
	{
		if (string.IsNullOrWhiteSpace(name))
			return Result<StudentName>.Failure(StudentErrors.NameEmptyError());

		if (string.IsNullOrWhiteSpace(surname))
			return Result<StudentName>.Failure(StudentErrors.SurnameEmptyError());

		if (name.Length > MAX_NAMES_LENGTH)
			return Result<StudentName>.Failure(StudentErrors.NameExceess(MAX_NAMES_LENGTH));

		if (surname.Length > MAX_NAMES_LENGTH)
			return Result<StudentName>.Failure(StudentErrors.SurnameExceess(MAX_NAMES_LENGTH));

		if (!string.IsNullOrWhiteSpace(patronymic) && patronymic.Length > MAX_NAMES_LENGTH)
			return Result<StudentName>.Failure(StudentErrors.PatronymicExceess(MAX_NAMES_LENGTH));

		return Result<StudentName>.Success(new StudentName(name, surname, patronymic.ValueOrEmpty()));
	}

	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return Name;
		yield return Surname;
		yield return string.IsNullOrWhiteSpace(Patronymic) ? string.Empty : Patronymic;
	}
}
