using StudentPerfomance.Application.EntitySchemas.Schemas.Students;

namespace StudentPerfomance.Application.EntitySchemas.Validators.StudentSchemaValidation;

public class StudentSchemaValidator : BaseSchemaValidator, ISchemaValidator
{
	private readonly Dictionary<ISchemaValidation<StudentSchema>, StudentSchema> _validations = [];
	public string Error => errorBuilder.ToString();
	public bool IsValid => isValid;
	public void ProcessValidation()
	{
		foreach (var validation in _validations)
		{
			StudentSchema schema = validation.Value;
			var criteria = validation.Key.BuildCriteria(schema);
			isValid = criteria(schema);
			if (!isValid)
			{
				AppendError(validation.Key.Error);
				break;
			}
		}
	}
	public StudentSchemaValidator WithNameValidation(StudentSchema schema)
	{
		ISchemaValidation<StudentSchema> validation = new StudentNameValidation(schema);
		_validations.Add(validation, schema);
		return this;
	}
	public StudentSchemaValidator WithStateValidation(StudentSchema schema)
	{
		ISchemaValidation<StudentSchema> validation = new StudentStateValidation(schema);
		_validations.Add(validation, schema);
		return this;
	}
	public StudentSchemaValidator WithRecordbookValidation(StudentSchema schema)
	{
		ISchemaValidation<StudentSchema> validation = new StudentRecordBookValidation(schema);
		_validations.Add(validation, schema);
		return this;
	}
}
