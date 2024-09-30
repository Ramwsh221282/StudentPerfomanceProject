using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;

namespace SPerfomance.Domain.Module.Shared.Common.Models.CompositeEntityValidator;

public sealed class CompositeValidator : BaseSchemaValidator, ISchemaValidator
{
	public List<ISchemaValidator> _validators = [];

	public CompositeValidator(params ISchemaValidator[] validators)
	{
		foreach (var validator in validators)
		{
			if (validator != null)
				_validators.Add(validator);
		}
	}
	public string Error => errorBuilder.ToString();
	public bool IsValid => isValid;
	public void ProcessValidation()
	{
		foreach (var validation in _validators)
		{
			validation.ProcessValidation();
			if (!validation.IsValid)
			{
				isValid = false;
				AppendError(validation.Error);
				break;
			}
		}
	}
}
