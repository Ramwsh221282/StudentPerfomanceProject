using CSharpFunctionalExtensions;

using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;
using SPerfomance.Domain.Module.Shared.Entities.Semesters.ValueObjects;

namespace SPerfomance.Application.Shared.Module.Schemas.Semesters.Validators;

internal sealed class SemesterNumberValidation(SemesterSchema semester) : BaseSchemaValidation, ISchemaValidation<SemesterSchema>
{
	private readonly SemesterSchema _semester = semester;
	public string Error => errorBuilder.ToString();

	public Func<EntitySchema, bool> BuildCriteria(SemesterSchema schema) => (schema) => Validate();

	protected override bool Validate()
	{
		Result<SemesterNumber> result = SemesterNumber.Create(_semester.Number);
		return result.IsFailure ? ReturnError(result.Error) : ReturnSuccess();
	}
}
