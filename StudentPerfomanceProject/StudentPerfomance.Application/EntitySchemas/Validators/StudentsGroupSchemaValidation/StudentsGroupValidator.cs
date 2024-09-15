using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Application.EntitySchemas.Validators.StudentSchemaValidation;

namespace StudentPerfomance.Application.EntitySchemas.Validators.StudentsGroupSchemaValidation;

public sealed class StudentsGroupValidator(StudentsGroupSchema schema) : BaseSchemaValidator, ISchemaValidator
{
	private readonly StudentsGroupSchema _schema = schema;
	public string Error => errorBuilder.ToString();
	public bool IsValid => isValid;
	public void ProcessValidation()
	{
		ISchemaValidation<StudentsGroupSchema> validation = new GroupNameValidation(_schema);
		var criteria = validation.BuildCriteria(_schema);
		isValid = criteria(_schema);
		AppendError(validation.Error);
	}
}
