using SPerfomance.Application.Shared.Module.Schemas.EducationDirections.Validators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;

namespace SPerfomance.Application.Shared.Module.Schemas.EducationPlans.Validators;

public sealed class EducationPlanValidator : BaseSchemaValidator, ISchemaValidator
{
	private readonly Dictionary<ISchemaValidation<EducationPlanSchema>, EducationPlanSchema> _validations = [];
	public string Error => errorBuilder.ToString();
	public bool IsValid => isValid;
	public void ProcessValidation()
	{
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
}
