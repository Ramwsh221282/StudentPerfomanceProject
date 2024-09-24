using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Application.EntitySchemas.Validators.StudentSchemaValidation;

namespace StudentPerfomance.Application.EntitySchemas.Validators.StudentsGroupSchemaValidation;

public sealed class StudentsGroupValidator(StudentsGroupSchema schema) : BaseSchemaValidator, ISchemaValidator
{
	private readonly ISchemaValidation<StudentsGroupSchema>[] _validations =
	[
		new GroupNameValidation(schema)];
	private readonly StudentsGroupSchema _schema = schema;
	public string Error => errorBuilder.ToString();
	public bool IsValid => isValid;
	public void ProcessValidation()
	{
		foreach (var validation in _validations)
		{
			var criteria = validation.BuildCriteria(_schema);
			if (!criteria(_schema))
			{
				AppendError(validation.Error);
			}
			isValid = criteria(_schema);
		}
	}
}
