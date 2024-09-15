namespace StudentPerfomance.Application.EntitySchemas.Validators.StudentSchemaValidation;

public interface ISchemaValidator
{
	string Error { get; }
	bool IsValid { get; }
	void ProcessValidation();
}
