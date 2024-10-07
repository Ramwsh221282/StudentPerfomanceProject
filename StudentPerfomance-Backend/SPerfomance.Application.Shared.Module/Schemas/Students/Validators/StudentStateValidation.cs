using CSharpFunctionalExtensions;

using SPerfomance.Domain.Module.Shared.Entities.Students.ValueObjects;

namespace SPerfomance.Application.Shared.Module.Schemas.Students.Validators;

internal sealed class StudentStateValidation(StudentSchema schema) : BaseSchemaValidation, ISchemaValidation<StudentSchema>
{
	private readonly StudentSchema _schema = schema;
	public string Error => errorBuilder.ToString();
	public Func<EntitySchema, bool> BuildCriteria(StudentSchema schema) => (schema) => Validate();
	protected override bool Validate()
	{
		Result<StudentState> state = StudentState.Create(_schema.State);
		return state.IsFailure ? ReturnError(state.Error) : ReturnSuccess();
	}
}
