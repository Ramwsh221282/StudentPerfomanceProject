using StudentPerfomance.Application.EntitySchemas.Schemas.TeacherDepartments;
using StudentPerfomance.Application.EntitySchemas.Validators.StudentSchemaValidation;

namespace StudentPerfomance.Application.EntitySchemas.Validators.DepartmentSchemaValidation;

public sealed class DepartmentValidator(DepartmentSchema schema) : BaseSchemaValidator, ISchemaValidator
{
	private readonly DepartmentSchema _schema = schema;
	public string Error => errorBuilder.ToString();
	public bool IsValid => isValid;
	public void ProcessValidation()
	{
		ISchemaValidation<DepartmentSchema> validation = new DepartmentNameValidation(_schema);
		var criteria = validation.BuildCriteria(_schema);
		isValid = criteria(_schema);
		AppendError(validation.Error);
	}
}
