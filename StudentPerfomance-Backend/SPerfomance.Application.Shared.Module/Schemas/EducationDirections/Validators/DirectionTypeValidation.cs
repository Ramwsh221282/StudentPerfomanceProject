using CSharpFunctionalExtensions;

using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections.ValueObjects;

namespace SPerfomance.Application.Shared.Module.Schemas.EducationDirections.Validators;

internal sealed class DirectionTypeValidation(EducationDirectionSchema schema) : BaseSchemaValidation, ISchemaValidation<EducationDirectionSchema>
{
	private readonly EducationDirectionSchema _schema = schema;
	public string Error => errorBuilder.ToString();
	public Func<EntitySchema, bool> BuildCriteria(EducationDirectionSchema schema) => (schema) => Validate();
	protected override bool Validate()
	{
		Result<DirectionType> result = DirectionType.Create(_schema.Type);
		return result.IsFailure ? ReturnError(result.Error) : ReturnSuccess();
	}
}
