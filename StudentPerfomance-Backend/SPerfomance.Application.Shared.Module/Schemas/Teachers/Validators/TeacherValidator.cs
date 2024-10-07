namespace SPerfomance.Application.Shared.Module.Schemas.Teachers.Validators;

public sealed class TeacherValidator : BaseSchemaValidator, ISchemaValidator
{
	private readonly Dictionary<ISchemaValidation<TeacherSchema>, TeacherSchema> _validations = [];
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
	public TeacherValidator WithJobTitle(TeacherSchema schema)
	{
		_validations.Add(new TeacherJobTitleValidation(schema), schema);
		return this;
	}

	public TeacherValidator WithNameValidation(TeacherSchema schema)
	{
		_validations.Add(new TeacherNameValidation(schema), schema);
		return this;
	}

	public TeacherValidator WithConditionValidation(TeacherSchema schema)
	{
		_validations.Add(new TeacherWorkingConditionValidation(schema), schema);
		return this;
	}
}
