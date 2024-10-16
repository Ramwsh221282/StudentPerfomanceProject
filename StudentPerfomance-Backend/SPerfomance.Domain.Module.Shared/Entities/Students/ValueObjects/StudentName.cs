using System.Text;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.ValueObjects;
using SPerfomance.Domain.Module.Shared.Entities.Students.Validators;
using SPerfomance.Domain.Module.Shared.Extensions;

namespace SPerfomance.Domain.Module.Shared.Entities.Students.ValueObjects;

public sealed class StudentName : ValueObject
{
	private StudentName()
	{
		Name = string.Empty;
		Surname = string.Empty;
		Thirdname = string.Empty;
	}

	private StudentName(string name, string surname, string thirdname)
	{
		Name = name.FormatSpaces().FirstCharacterToUpper();
		Surname = surname.FormatSpaces().FirstCharacterToUpper();
		Thirdname = thirdname.FormatSpaces().FirstCharacterToUpper();
	}

	public string Name { get; }

	public string Surname { get; }

	public string Thirdname { get; }
	public static StudentName CreateDefault() => new StudentName();
	public static CSharpFunctionalExtensions.Result<StudentName> Create(string name, string surname, string thirdname)
	{
		StudentName result = new StudentName(name, surname, thirdname);
		Validator<StudentName> validator = new StudentNameValidator(result);
		return validator.Validate() ? result : CSharpFunctionalExtensions.Result.Failure<StudentName>(validator.GetErrorText());
	}

	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return Name;
		yield return Surname;
		yield return Thirdname == null ? string.Empty : Thirdname;
	}

	public override string ToString() =>
		new StringBuilder()
		.Append(Name)
		.Append(" ")
		.Append(Surname)
		.Append(" ")
		.Append(Thirdname)
		.ToString();
}
