using System.Text.RegularExpressions;
using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.Users.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.Users.ValueObjects;

public class UserEmail : DomainValueObject
{
    private static Regex _emailPattern = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

    public string Email { get; private set; }

    private UserEmail() => Email = string.Empty;

    private UserEmail(string email) => Email = email;

    internal static UserEmail Empty => new UserEmail();

    internal static Result<UserEmail> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return Result<UserEmail>.Failure(UserErrors.EmailEmpty());

        return !_emailPattern.Match(email).Success
            ? Result<UserEmail>.Failure(UserErrors.EmailInvalid(email))
            : Result<UserEmail>.Success(new(email));
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Email;
    }
}
