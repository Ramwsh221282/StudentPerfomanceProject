using StudentPerfomance.Application.EntitySchemas.Schemas.EducationPlans;
using StudentPerfomance.Application.EntitySchemas.Validators.EducationDirectionsValidations;
using StudentPerfomance.Application.EntitySchemas.Validators.StudentSchemaValidation;

namespace StudentPerfomance.Application.EntitySchemas.Validators.EducationPlansValidations;

internal sealed class EducationPlanValidator : BaseSchemaValidator, ISchemaValidator
{
	private readonly Dictionary<ISchemaValidation<EducationPlanSchema>, EducationPlanSchema> _validations = [];
	private EducationDirectionValidator? _directionValidator = null;
	public string Error => errorBuilder.ToString();
	public bool IsValid => isValid;

	public void ProcessValidation()
	{
		if (_directionValidator != null)
		{
			_directionValidator.ProcessValidation();
			isValid = _directionValidator.IsValid;
			if (!isValid) AppendError(_directionValidator.Error);
		}
		foreach (var validation in _validations)
		{
			EducationPlanSchema schema = validation.Value;
			var criteria = validation.Key.BuildCriteria(schema);
			isValid = criteria(schema);
			if (!isValid)
			{
				AppendError(validation.Key.Error);
				break;
			}
		}
	}

	public EducationPlanValidator WithYearValidation(EducationPlanSchema schema)
	{
		_validations.Add(new EducationPlanYearValidation(schema), schema);
		return this;
	}

	public EducationPlanValidator WithDirectionValidation(EducationPlanSchema schema)
	{
		_directionValidator = new EducationDirectionValidator()
		.WithNameValidation(schema.Direction)
		.WithCodeValidator(schema.Direction)
		.WithTypeValidation(schema.Direction);
		return this;
	}
}
