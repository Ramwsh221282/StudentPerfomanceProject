using System.Text.RegularExpressions;
using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.StudentGroups.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.StudentGroups.ValueObjects;

public class StudentGroupName : DomainValueObject
{
    private const int MinNameLength = 3;

    private const int MaxNameLength = 15;

    private static readonly Regex Pattern = new Regex(@"^[А-Я]+ \d{2}-\d{2}$");

    public string Name { get; }

    internal StudentGroupName() => Name = string.Empty;

    internal StudentGroupName(string name) => Name = name;

    internal static StudentGroupName Empty => new StudentGroupName();

    internal static Result<StudentGroupName> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<StudentGroupName>.Failure(StudentGroupErrors.NameEmpty());

        return name.Length switch
        {
            > MaxNameLength => Result<StudentGroupName>.Failure(
                StudentGroupErrors.NameExceess(MaxNameLength)
            ),
            < MinNameLength => Result<StudentGroupName>.Failure(
                StudentGroupErrors.NameLess(MinNameLength)
            ),
            _ => !Pattern.Match(name).Success
                ? Result<StudentGroupName>.Failure(StudentGroupErrors.NameInvalid(name))
                : Result<StudentGroupName>.Success(new StudentGroupName(name)),
        };
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }

    public override string ToString() => $"{Name}";
}
