using CSharpFunctionalExtensions;

using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans.ValueObjects;

namespace SPerfomance.Application.Shared.Module.Schemas.SemesterPlans.Validators;

internal sealed class SemesterPlanDisciplineNameValidation(SemesterPlanSchema schema) : BaseSchemaValidation, ISchemaValidation<SemesterPlanSchema>
{
	private readonly SemesterPlanSchema _schema = schema;
	public string Error => errorBuilder.ToString();

	public Func<EntitySchema, bool> BuildCriteria(SemesterPlanSchema schema) => (schema) => Validate();

	protected override bool Validate()
	{
		Result<Discipline> result = Discipline.Create(_schema.Discipline);
		return result.IsFailure ? ReturnError(result.Error) : ReturnSuccess();
	}
}
