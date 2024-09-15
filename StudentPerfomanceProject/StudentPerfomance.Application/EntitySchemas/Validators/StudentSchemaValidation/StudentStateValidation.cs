using CSharpFunctionalExtensions;

using StudentPerfomance.Application.EntitySchemas.Schemas;
using StudentPerfomance.Application.EntitySchemas.Schemas.Students;
using StudentPerfomance.Domain.ValueObjects;

namespace StudentPerfomance.Application.EntitySchemas.Validators.StudentSchemaValidation;

public sealed class StudentStateValidation(StudentSchema schema) : BaseSchemaValidation, ISchemaValidation<StudentSchema>
{
	private readonly StudentSchema _schema = schema;
	public string Error => errorBuilder.ToString();
	public Func<EntitySchema, bool> BuildCriteria(StudentSchema schema) => (schema) => Validate();

	protected override bool Validate()
	{
		Result<StudentState> result = StudentState.Create(_schema.State);
		if (result.IsFailure)
		{
			errorBuilder.Append(result.Error);
			return false;
		}
		return true;
	}
}
