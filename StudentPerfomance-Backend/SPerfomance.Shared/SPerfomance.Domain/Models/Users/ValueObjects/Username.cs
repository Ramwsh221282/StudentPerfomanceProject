using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.Users.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.Users.ValueObjects;

public class Username : DomainValueObject
{
    private const int _maxNameLength = 40;

    public string Name { get; private set; }

    public string Surname { get; private set; }

    public string Patronymic { get; private set; }

    private Username()
    {
        Name = string.Empty;
        Surname = string.Empty;
        Patronymic = string.Empty;
    }

    private Username(string name, string surname, string patronymic)
    {
        Name = name.Trim();
        Surname = surname.Trim();
        Patronymic = string.IsNullOrWhiteSpace(patronymic) ? string.Empty : patronymic.Trim();
    }

    internal static Username Empty => new Username();

    public static Result<Username> Create(string name, string surname, string? patronymic)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<Username>.Failure(UserErrors.NameEmpty());

        if (string.IsNullOrWhiteSpace(surname))
            return Result<Username>.Failure(UserErrors.SurnameEmpty());

        if (name.Length > _maxNameLength)
            return Result<Username>.Failure(UserErrors.NameExceess(_maxNameLength));

        if (surname.Length > _maxNameLength)
            return Result<Username>.Failure(UserErrors.SurnameExcees(_maxNameLength));

        if (!string.IsNullOrWhiteSpace(patronymic) && patronymic.Length > _maxNameLength)
            return Result<Username>.Failure(UserErrors.ThirdnameExcess(_maxNameLength));

        return Result<Username>.Success(new(name, surname, patronymic.ValueOrEmpty()));
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Surname;
        yield return Patronymic;
    }
}
