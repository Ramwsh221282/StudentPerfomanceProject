using StudentPerfomance.Application.EntitySchemas.Schemas.EducationDirections;
using StudentPerfomance.Application.EntitySchemas.Validators.StudentSchemaValidation;

namespace StudentPerfomance.Application.EntitySchemas.Validators.EducationDirectionsValidations;

internal class EducationDirectionValidator : BaseSchemaValidator, ISchemaValidator
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
		_validations.Add(new EducationDirectionNameValidation(schema), schema);
		return this;
	}

	public EducationDirectionValidator WithCodeValidator(EducationDirectionSchema schema)
	{
		_validations.Add(new EducationDirectionCodeValidation(schema), schema);
		return this;
	}

	public EducationDirectionValidator WithTypeValidation(EducationDirectionSchema schema)
	{
		_validations.Add(new EducationDirectionTypeValidation(schema), schema);
		return this;
	}
}
