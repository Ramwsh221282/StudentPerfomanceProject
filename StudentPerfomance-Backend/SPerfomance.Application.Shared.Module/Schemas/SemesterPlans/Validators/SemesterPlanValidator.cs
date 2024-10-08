namespace SPerfomance.Application.Shared.Module.Schemas.SemesterPlans.Validators;

public sealed class SemesterPlanValidator : BaseSchemaValidator, ISchemaValidator
{
	private readonly Dictionary<ISchemaValidation<SemesterPlanSchema>, SemesterPlanSchema> _validations = [];
	public string Error => errorBuilder.ToString();
	public bool IsValid => isValid;

	public void ProcessValidation()
	{
		foreach (var validation in _validations)
		{
			var schema = validation.Value;
			var criteria = validation.Key.BuildCriteria(schema);
			isValid = criteria(schema);
			if (!isValid)
			{
				AppendError(validation.Key.Error);
				break;
			}
		}
	}

	public SemesterPlanValidator WithDisciplineValidation(SemesterPlanSchema schema)
	{
		_validations.Add(new SemesterPlanDisciplineNameValidation(schema), schema);
		return this;
	}
}
