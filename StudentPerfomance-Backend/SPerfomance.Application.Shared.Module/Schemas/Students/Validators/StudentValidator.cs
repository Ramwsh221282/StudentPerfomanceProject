namespace SPerfomance.Application.Shared.Module.Schemas.Students.Validators;

public sealed class StudentValidator : BaseSchemaValidator, ISchemaValidator
{
	private readonly Dictionary<ISchemaValidation<StudentSchema>, StudentSchema> _validations = [];
	public string Error => errorBuilder.ToString();
	public bool IsValid => isValid;

	public void ProcessValidation()
	{
		foreach (var validation in _validations)
		{
			var schema = validation.Value;
			var criteria = validation.Key.BuildCriteria(schema);
			isValid = criteria(schema);
			if (!isValid)
			{
				AppendError(validation.Key.Error);
				break;
			}
		}
	}

	public StudentValidator WithNameValidation(StudentSchema schema)
	{
		_validations.Add(new StudentNameValidation(schema), schema);
		return this;
	}

	public StudentValidator WithStateValidation(StudentSchema schema)
	{
		_validations.Add(new StudentNameValidation(schema), schema);
		return this;
	}

	public StudentValidator WithRecordbookValidation(StudentSchema schema)
	{
		_validations.Add(new StudentRecordBookValidation(schema), schema);
		return this;
	}
}
