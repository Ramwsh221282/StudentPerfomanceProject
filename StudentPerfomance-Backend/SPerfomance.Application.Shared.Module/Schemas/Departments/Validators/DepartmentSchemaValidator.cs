namespace SPerfomance.Application.Shared.Module.Schemas.Departments.Validators;

public sealed class DepartmentSchemaValidator : BaseSchemaValidator, ISchemaValidator
{
	private readonly Dictionary<ISchemaValidation<DepartmentSchema>, DepartmentSchema> _validations = [];
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
	public DepartmentSchemaValidator WithNameValidation(DepartmentSchema schema)
	{
		_validations.Add(new DepartmentNameValidation(schema), schema);
		return this;
	}
}
