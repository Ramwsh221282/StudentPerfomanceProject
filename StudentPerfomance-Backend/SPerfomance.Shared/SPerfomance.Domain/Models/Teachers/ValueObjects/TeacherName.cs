using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.Teachers.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.Teachers.ValueObjects;

public class TeacherName : DomainValueObject
{
    private const int MaxNamesLength = 40;

    public string Name { get; }

    public string Surname { get; }

    public string Patronymic { get; }

    private TeacherName()
    {
        Name = string.Empty;
        Surname = string.Empty;
        Patronymic = string.Empty;
    }

    private TeacherName(string name, string surname, string patronymic)
    {
        Name = name.Trim();
        Surname = surname.Trim();
        Patronymic = string.IsNullOrWhiteSpace(patronymic) ? string.Empty : patronymic.Trim();
    }

    internal static TeacherName Empty => new TeacherName();

    internal static Result<TeacherName> Create(string name, string surname, string? patronymic)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<TeacherName>.Failure(TeacherErrors.NameEmpty());

        if (string.IsNullOrWhiteSpace(surname))
            return Result<TeacherName>.Failure(TeacherErrors.SurnameEmpty());

        if (name.Length > MaxNamesLength)
            return Result<TeacherName>.Failure(TeacherErrors.NameExceess(MaxNamesLength));

        if (surname.Length > MaxNamesLength)
            return Result<TeacherName>.Failure(TeacherErrors.SurnameExceess(MaxNamesLength));

        if (!string.IsNullOrWhiteSpace(patronymic) && patronymic.Length > MaxNamesLength)
            return Result<TeacherName>.Failure(TeacherErrors.PatronymicExceess(MaxNamesLength));

        return Result<TeacherName>.Success(
            new TeacherName(name, surname, patronymic.ValueOrEmpty())
        );
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Surname;
        yield return Patronymic;
    }

    public override string ToString() => $"{Surname} {Name[0]} {Patronymic[0]}";
}
