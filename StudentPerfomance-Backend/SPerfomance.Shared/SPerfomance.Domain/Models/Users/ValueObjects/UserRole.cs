using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.Users.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.Users.ValueObjects;

public class UserRole : DomainValueObject
{
    private static UserRole[] _roles = [new("Администратор"), new("Преподаватель")];

    public static UserRole Administrator = new("Администратор");

    public static UserRole Teacher = new("Преподаватель");

    public string Role { get; private set; }

    private UserRole() => Role = string.Empty;

    private UserRole(string role) => Role = role.Trim();

    internal static UserRole Empty => new UserRole();

    internal static Result<UserRole> Create(string role)
    {
        if (string.IsNullOrWhiteSpace(role))
            return Result<UserRole>.Failure(UserErrors.RoleEmpty());

        return _roles.Any(r => r.Role == role) == false
            ? Result<UserRole>.Failure(UserErrors.RoleInvalid(role))
            : Result<UserRole>.Success(new(role));
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Role;
    }
}
