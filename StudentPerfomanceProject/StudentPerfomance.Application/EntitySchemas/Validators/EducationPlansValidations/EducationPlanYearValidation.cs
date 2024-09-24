using CSharpFunctionalExtensions;

using StudentPerfomance.Application.EntitySchemas.Schemas;
using StudentPerfomance.Application.EntitySchemas.Schemas.EducationPlans;
using StudentPerfomance.Domain.ValueObjects.EducationPlans;

namespace StudentPerfomance.Application.EntitySchemas.Validators.EducationPlansValidations;

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
