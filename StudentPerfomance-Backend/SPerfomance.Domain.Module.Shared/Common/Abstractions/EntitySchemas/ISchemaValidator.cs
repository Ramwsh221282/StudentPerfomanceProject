namespace SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;

public interface ISchemaValidator
{
	string Error { get; }
	bool IsValid { get; }
	void ProcessValidation();
}
