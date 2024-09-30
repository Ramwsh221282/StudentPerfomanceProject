using CSharpFunctionalExtensions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans.ValueObjects;

namespace SPerfomance.Application.Shared.Module.Schemas.EducationPlans.Validators;

internal sealed class EducationPlanYearValidation(EducationPlanSchema schema) : BaseSchemaValidation, ISchemaValidation<EducationPlanSchema>
{
	private readonly EducationPlanSchema _schema = schema;
	public string Error => errorBuilder.ToString();
	public Func<EntitySchema, bool> BuildCriteria(EducationPlanSchema schema) => (schema) => Validate();
	protected override bool Validate()
	{
		Result<YearOfCreation> result = YearOfCreation.Create(_schema.Year);
		return result.IsFailure ? ReturnError(result.Error) : ReturnSuccess();
	}
}
