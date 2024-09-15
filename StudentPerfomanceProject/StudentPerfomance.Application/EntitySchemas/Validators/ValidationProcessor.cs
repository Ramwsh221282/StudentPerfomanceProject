using System.Text;
using StudentPerfomance.Application.EntitySchemas.Validators.StudentSchemaValidation;

namespace StudentPerfomance.Application.EntitySchemas.Validators;

public sealed class ValidationProcessor
{
	private readonly StringBuilder errorMessage = new StringBuilder();
	public string Error => errorMessage.ToString();
	public bool IsValid(ISchemaValidator validator)
	{
		validator.ProcessValidation();
		errorMessage.Append(validator.Error);
		return validator.IsValid;
	}
}
