using StudentPerfomance.Application.EntitySchemas.Schemas.Discplines;
using StudentPerfomance.Application.EntitySchemas.Validators.StudentSchemaValidation;

namespace StudentPerfomance.Application.EntitySchemas.Validators.Disciplines;

public sealed class DisciplineValidator(DisciplineSchema schema) : BaseSchemaValidator, ISchemaValidator
{
	private readonly DisciplineSchema _schema = schema;
	public string Error => errorBuilder.ToString();
	public bool IsValid => isValid;
	public void ProcessValidation()
	{
		ISchemaValidation<DisciplineSchema> validation = new DisciplineValidation(_schema);
		var criteria = validation.BuildCriteria(_schema);
		isValid = criteria(_schema);
		if (!isValid)
			AppendError(validation.Error);
	}
}
