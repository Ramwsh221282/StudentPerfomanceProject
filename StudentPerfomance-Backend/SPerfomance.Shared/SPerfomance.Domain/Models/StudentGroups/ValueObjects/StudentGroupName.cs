using System.Text.RegularExpressions;

using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.StudentGroups.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.StudentGroups.ValueObjects;

public class StudentGroupName : DomainValueObject
{
	private const int MIN_NAME_LENGTH = 3;

	private const int MAX_NAME_LENGTH = 15;

	private static Regex _pattern = new Regex(@"^[А-Я]+ \d{2}-\d{2}$");

	public string Name { get; private set; }

	internal StudentGroupName() => Name = string.Empty;

	internal StudentGroupName(string name) => Name = name;

	internal static StudentGroupName Empty => new StudentGroupName();

	internal static Result<StudentGroupName> Create(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
			return Result<StudentGroupName>.Failure(StudentGroupErrors.NameEmpty());

		if (name.Length > MAX_NAME_LENGTH)
			return Result<StudentGroupName>.Failure(StudentGroupErrors.NameExceess(MAX_NAME_LENGTH));

		if (name.Length < MIN_NAME_LENGTH)
			return Result<StudentGroupName>.Failure(StudentGroupErrors.NameLess(MIN_NAME_LENGTH));

		if (!_pattern.Match(name).Success)
			return Result<StudentGroupName>.Failure(StudentGroupErrors.NameInvalid(name));

		return Result<StudentGroupName>.Success(new StudentGroupName(name));
	}

	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return Name;
	}

	public override string ToString() => $"{Name}";
}
