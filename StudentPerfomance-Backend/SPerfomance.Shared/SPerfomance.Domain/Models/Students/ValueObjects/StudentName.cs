using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.Students.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.Students.ValueObjects;

public class StudentName : DomainValueObject
{
    public const int MaxNamesLength = 40;
    public string Name { get; }
    public string Surname { get; }
    public string Patronymic { get; }

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

        if (name.Length > MaxNamesLength)
            return Result<StudentName>.Failure(StudentErrors.NameExceess(MaxNamesLength));

        if (surname.Length > MaxNamesLength)
            return Result<StudentName>.Failure(StudentErrors.SurnameExceess(MaxNamesLength));

        if (!string.IsNullOrWhiteSpace(patronymic) && patronymic.Length > MaxNamesLength)
            return Result<StudentName>.Failure(StudentErrors.PatronymicExceess(MaxNamesLength));

        return Result<StudentName>.Success(
            new StudentName(name, surname, patronymic.ValueOrEmpty())
        );
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Surname;
        yield return string.IsNullOrWhiteSpace(Patronymic) ? string.Empty : Patronymic;
    }

    public override string ToString() => $"{Surname} {Name[0]} {Patronymic[0]}";
}
