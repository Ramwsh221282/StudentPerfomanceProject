namespace SPerfomance.Application.Shared.Module.Schemas.EducationDirections.Validators;

public sealed class EducationDirectionValidator : BaseSchemaValidator, ISchemaValidator
{
	private readonly Dictionary<ISchemaValidation<EducationDirectionSchema>, EducationDirectionSchema> _validations = [];
	public string Error => errorBuilder.ToString();
	public bool IsValid => isValid;

	public void ProcessValidation()
	{
		foreach (var validation in _validations)
		{
			EducationDirectionSchema schema = validation.Value;
			var criteria = validation.Key.BuildCriteria(schema);
			isValid = criteria(schema);
			if (!isValid)
			{
				AppendError(validation.Key.Error);
				break;
			}
		}
	}

	public EducationDirectionValidator WithNameValidation(EducationDirectionSchema schema)
	{
		_validations.Add(new DirectionNameValidation(schema), schema);
		return this;
	}

	public EducationDirectionValidator WithCodeValidator(EducationDirectionSchema schema)
	{
		_validations.Add(new DirectionCodeValidation(schema), schema);
		return this;
	}

	public EducationDirectionValidator WithTypeValidation(EducationDirectionSchema schema)
	{
		_validations.Add(new DirectionTypeValidation(schema), schema);
		return this;
	}
}
