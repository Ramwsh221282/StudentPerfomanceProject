using CSharpFunctionalExtensions;

using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;
using SPerfomance.Domain.Module.Shared.Entities.Teachers.ValueObjects;

namespace SPerfomance.Application.Shared.Module.Schemas.Teachers.Validators;

internal sealed class TeacherWorkingConditionValidation(TeacherSchema schema) : BaseSchemaValidation, ISchemaValidation<TeacherSchema>
{
	private readonly TeacherSchema _schema = schema;
	public string Error => errorBuilder.ToString();
	public Func<EntitySchema, bool> BuildCriteria(TeacherSchema schema) => (schema) => Validate();
	protected override bool Validate()
	{
		Result<WorkingCondition> result = WorkingCondition.Create(_schema.Condition);
		return result.IsFailure ? ReturnError(result.Error) : ReturnSuccess();
	}
}
