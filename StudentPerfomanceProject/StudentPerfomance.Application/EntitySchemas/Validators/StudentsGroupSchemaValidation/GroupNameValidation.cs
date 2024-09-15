using CSharpFunctionalExtensions;

using StudentPerfomance.Application.EntitySchemas.Schemas;
using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Domain.ValueObjects.StudentGroup;

namespace StudentPerfomance.Application.EntitySchemas.Validators.StudentsGroupSchemaValidation;

public sealed class GroupNameValidation(StudentsGroupSchema schema) : BaseSchemaValidation, ISchemaValidation<StudentsGroupSchema>
{
	private readonly StudentsGroupSchema _schema = schema;
	public string Error => errorBuilder.ToString();
	public Func<EntitySchema, bool> BuildCriteria(StudentsGroupSchema schema) => (schema) => Validate();
	protected override bool Validate()
	{
		Result<GroupName> result = GroupName.Create(_schema.Name);
		if (result.IsFailure)
		{
			AppendErrorText(result.Error);
			return false;
		}
		return true;
	}
}
