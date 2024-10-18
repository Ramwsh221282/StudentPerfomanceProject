using System.Text.RegularExpressions;

using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Entities.Users.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Users.ValueObjects;

namespace SPerfomance.Domain.Module.Shared.Entities.Users.Validators;

internal sealed class EmailValidator(EmailValueObject email) : Validator<EmailValueObject>
{
	private readonly EmailValueObject _email = email;

	private const string _pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

	public override bool Validate()
	{
		Regex regex = new Regex(_pattern);
		Match match = regex.Match(_email.Email);
		if (!match.Success)
			error.AppendError(new EmailInputError());
		return HasError;
	}
}
