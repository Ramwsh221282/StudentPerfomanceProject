using CSharpFunctionalExtensions;
using StudentPerfomance.Domain.Validators;
using StudentPerfomance.Domain.Validators.Student;

namespace StudentPerfomance.Domain.ValueObjects.Student;

public class StudentName : ValueObject
{
	private StudentName() { }

	private StudentName(string name, string surname, string? thirdname) =>
		(Name, Surname, Thirdname) = (name, surname, thirdname);

	public string Name { get; }

	public string Surname { get; }

	public string? Thirdname { get; }

	public static Result<StudentName> Create(string name, string surname, string? thirdname)
	{
		Validator<StudentName> validator = new StudentNameValidator(name, surname, thirdname);
		if (!validator.Validate())
			return Result.Failure<StudentName>(validator.GetErrorText());
		return new StudentName(name, surname, thirdname);
	}

	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return Name;
		yield return Surname;
		yield return Thirdname;
	}
}
