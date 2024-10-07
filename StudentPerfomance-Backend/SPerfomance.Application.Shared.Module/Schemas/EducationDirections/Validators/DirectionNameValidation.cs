using CSharpFunctionalExtensions;

using SPerfomance.Domain.Module.Shared.Entities.EducationDirections.ValueObjects;

namespace SPerfomance.Application.Shared.Module.Schemas.EducationDirections.Validators;

internal sealed class DirectionNameValidation(EducationDirectionSchema schema) : BaseSchemaValidation, ISchemaValidation<EducationDirectionSchema>
{
	private readonly EducationDirectionSchema _schema = schema;
	public string Error => errorBuilder.ToString();
	public Func<EntitySchema, bool> BuildCriteria(EducationDirectionSchema schema) => (schema) => Validate();
	protected override bool Validate()
	{
		Result<DirectionName> result = DirectionName.Create(_schema.Name);
		return result.IsFailure ? ReturnError(result.Error) : ReturnSuccess();
	}
}
