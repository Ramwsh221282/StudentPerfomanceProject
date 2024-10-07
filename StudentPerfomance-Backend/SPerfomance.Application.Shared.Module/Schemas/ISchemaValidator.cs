namespace SPerfomance.Application.Shared.Module.Schemas;

public interface ISchemaValidator
{
	string Error { get; }
	bool IsValid { get; }
	void ProcessValidation();
}
