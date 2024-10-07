using CSharpFunctionalExtensions;

using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.ValueObjects;

namespace SPerfomance.Application.Shared.Module.Schemas.StudentGroups.Validators;

internal sealed class GroupNameValidation(StudentsGroupSchema schema) : BaseSchemaValidation, ISchemaValidation<StudentsGroupSchema>
{
	private readonly StudentsGroupSchema _schema = schema;
	public string Error => errorBuilder.ToString();
	public Func<EntitySchema, bool> BuildCriteria(StudentsGroupSchema schema) => (schema) => Validate();
	protected override bool Validate()
	{
		Result<GroupName> result = GroupName.Create(_schema.Name);
		return result.IsFailure ? ReturnError(result.Error) : ReturnSuccess();
	}
}
