using CSharpFunctionalExtensions;
using StudentPerfomance.Domain.Validators;
using StudentPerfomance.Domain.Validators.Teacher;

namespace StudentPerfomance.Domain.ValueObjects.Teacher;

public class TeacherName : ValueObject
{
	private TeacherName() { }

	private TeacherName(string name, string surname, string? thirdname) =>
		(Name, Surname, Thirdname) = (name, surname, thirdname);

	public string Name { get; }
	public string Surname { get; }
	public string? Thirdname { get; }

	public static Result<TeacherName> Create(string name, string surname, string? thirdname)
	{
		Validator<TeacherName> validator = new TeacherNameValidator(name, surname, thirdname);
		if (!validator.Validate())
			return Result.Failure<TeacherName>(validator.GetErrorText());
		return new TeacherName(name, surname, thirdname);
	}

	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return Name;
		yield return Surname;
		yield return Thirdname;
	}
}
