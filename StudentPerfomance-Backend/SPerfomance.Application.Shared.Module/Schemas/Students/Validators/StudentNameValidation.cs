using CSharpFunctionalExtensions;

using SPerfomance.Domain.Module.Shared.Entities.Students.ValueObjects;

namespace SPerfomance.Application.Shared.Module.Schemas.Students.Validators;

internal sealed class StudentNameValidation(StudentSchema schema) : BaseSchemaValidation, ISchemaValidation<StudentSchema>
{
	private readonly StudentSchema _schema = schema;
	public string Error => errorBuilder.ToString();
	public Func<EntitySchema, bool> BuildCriteria(StudentSchema schema) => (schema) => Validate();
	protected override bool Validate()
	{
		Result<StudentName> result = StudentName.Create(_schema.Name, _schema.Surname, _schema.Thirdname);
		return result.IsFailure ? ReturnError(result.Error) : ReturnSuccess();
	}
}
