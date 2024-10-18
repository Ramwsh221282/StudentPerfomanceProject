using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.ValueObjects;
using SPerfomance.Domain.Module.Shared.Entities.Users.Validators;

namespace SPerfomance.Domain.Module.Shared.Entities.Users.ValueObjects;

public sealed class EmailValueObject : ValueObject
{
	private EmailValueObject() => Email = string.Empty;

	private EmailValueObject(string email) => Email = email;

	public string Email { get; private set; }

	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return Email;
	}

	public CSharpFunctionalExtensions.Result ChangeEmail(EmailValueObject updated)
	{
		if (this == updated) return CSharpFunctionalExtensions.Result.Failure("Данные идентичны");
		Email = updated.Email;
		return CSharpFunctionalExtensions.Result.Success();
	}

	public static EmailValueObject CreateDefault() => new EmailValueObject();

	public static CSharpFunctionalExtensions.Result<EmailValueObject> Create(string email)
	{
		EmailValueObject result = new EmailValueObject(email);
		Validator<EmailValueObject> validator = new EmailValidator(result);
		return validator.Validate() ?
			result :
			CSharpFunctionalExtensions.Result.Failure<EmailValueObject>(validator.GetErrorText());
	}
}
