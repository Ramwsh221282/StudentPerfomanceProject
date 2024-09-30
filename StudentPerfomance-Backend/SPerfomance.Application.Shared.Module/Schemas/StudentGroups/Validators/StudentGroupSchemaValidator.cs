using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;

namespace SPerfomance.Application.Shared.Module.Schemas.StudentGroups.Validators;

public sealed class StudentGroupSchemaValidator : BaseSchemaValidator, ISchemaValidator
{
	private readonly Dictionary<ISchemaValidation<StudentsGroupSchema>, StudentsGroupSchema> _validations = [];
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

	public StudentGroupSchemaValidator WithNameValidation(StudentsGroupSchema schema)
	{
		if (!string.IsNullOrWhiteSpace(schema.NameInfo))
			_validations.Add(new GroupNameValidation(schema), schema);
		return this;
	}
}
