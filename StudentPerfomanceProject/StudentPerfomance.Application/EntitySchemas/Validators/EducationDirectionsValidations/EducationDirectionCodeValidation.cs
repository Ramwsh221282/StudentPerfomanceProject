using CSharpFunctionalExtensions;

using StudentPerfomance.Application.EntitySchemas.Schemas;
using StudentPerfomance.Application.EntitySchemas.Schemas.EducationDirections;
using StudentPerfomance.Domain.ValueObjects.EducationDirections;

namespace StudentPerfomance.Application.EntitySchemas.Validators.EducationDirectionsValidations;

internal sealed class EducationDirectionCodeValidation(EducationDirectionSchema schema) : BaseSchemaValidation, ISchemaValidation<EducationDirectionSchema>
{
	private readonly EducationDirectionSchema _schema = schema;
	public string Error => errorBuilder.ToString();
	public Func<EntitySchema, bool> BuildCriteria(EducationDirectionSchema schema) => (schema) => Validate();
	protected override bool Validate()
	{
		Result<DirectionCode> result = DirectionCode.Create(_schema.Code);
		return result.IsFailure ? ReturnError(result.Error) : ReturnSuccess();
	}
}
