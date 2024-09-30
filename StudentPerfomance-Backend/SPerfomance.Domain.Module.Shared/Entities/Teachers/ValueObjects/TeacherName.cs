using System.Text;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.ValueObjects;
using SPerfomance.Domain.Module.Shared.Entities.Teachers.Validators;

namespace SPerfomance.Domain.Module.Shared.Entities.Teachers.ValueObjects;

public sealed class TeacherName : ValueObject
{
	private TeacherName()
	{
		Name = string.Empty;
		Surname = string.Empty;
		Thirdname = string.Empty;
	}

	private TeacherName(string name, string surname, string thirdname) =>
		(Name, Surname, Thirdname) = (name, surname, thirdname);

	public string Name { get; }
	public string Surname { get; }
	public string Thirdname { get; }
	public static TeacherName CreateDefault() => new TeacherName();
	public static CSharpFunctionalExtensions.Result<TeacherName> Create(string name, string surname, string thirdname)
	{
		Validator<TeacherName> validator = new TeacherNameValidator(name, surname, thirdname);
		return validator.Validate() ? new TeacherName(name, surname, thirdname) :
			CSharpFunctionalExtensions.Result.Failure<TeacherName>(validator.GetErrorText());
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
