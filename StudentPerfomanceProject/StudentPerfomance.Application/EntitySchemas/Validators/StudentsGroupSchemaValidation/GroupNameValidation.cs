using StudentPerfomance.Application.EntitySchemas.Schemas;
using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;

namespace StudentPerfomance.Application.EntitySchemas.Validators.StudentsGroupSchemaValidation;

internal sealed class GroupNameValidation(StudentsGroupSchema schema) : BaseSchemaValidation, ISchemaValidation<StudentsGroupSchema>
{
	private readonly StudentsGroupSchema _schema = schema;
	public string Error => errorBuilder.ToString();
	public Func<EntitySchema, bool> BuildCriteria(StudentsGroupSchema schema) => (schema) => Validate();
	protected override bool Validate()
	{
		if (_schema.Name.IsFailure)
		{
			AppendErrorText(_schema.Name.Error);
			return false;
		}
		return true;
	}
}
