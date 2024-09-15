using StudentPerfomance.Application.EntitySchemas.Schemas.Teachers;
using StudentPerfomance.Application.EntitySchemas.Validators.StudentSchemaValidation;

namespace StudentPerfomance.Application.EntitySchemas.Validators.TeachersValidation;

public sealed class TeacherSchemaValidator(TeacherSchema schema) : BaseSchemaValidator, ISchemaValidator
{
	private readonly TeacherSchema _schema = schema;
	public string Error => errorBuilder.ToString();
	public bool IsValid => isValid;
	public void ProcessValidation()
	{
		ISchemaValidation<TeacherSchema> validation = new TeacherNameValidation(_schema);
		var criteria = validation.BuildCriteria(_schema);
		isValid = criteria(_schema);
		AppendError(validation.Error);
	}
}
