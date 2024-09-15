using StudentPerfomance.Application.EntitySchemas.Schemas.Semesters;
using StudentPerfomance.Application.EntitySchemas.Validators.StudentSchemaValidation;

namespace StudentPerfomance.Application.EntitySchemas.Validators.SemesterValidation;

public sealed class SemesterValidator(SemesterSchema schema) : BaseSchemaValidator, ISchemaValidator
{
	private readonly SemesterSchema _schema = schema;
	public string Error => errorBuilder.ToString();
	public bool IsValid => isValid;
	public void ProcessValidation()
	{
		ISchemaValidation<SemesterSchema> validation = new SemesterNumberValidation(_schema);
		var criteria = validation.BuildCriteria(_schema);
		isValid = criteria(_schema);
		AppendError(validation.Error);
	}
}
