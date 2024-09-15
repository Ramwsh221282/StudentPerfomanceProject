using CSharpFunctionalExtensions;

using StudentPerfomance.Application.EntitySchemas.Schemas;
using StudentPerfomance.Application.EntitySchemas.Schemas.Semesters;
using StudentPerfomance.Domain.ValueObjects.SemesterValueObjects;

namespace StudentPerfomance.Application.EntitySchemas.Validators.SemesterValidation;

public sealed class SemesterNumberValidation(SemesterSchema schema) : BaseSchemaValidation, ISchemaValidation<SemesterSchema>
{
	private readonly SemesterSchema _schema = schema;
	public string Error => errorBuilder.ToString();

	public Func<EntitySchema, bool> BuildCriteria(SemesterSchema schema) => (schema) => Validate();

	protected override bool Validate()
	{
		Result<SemesterNumber> result = SemesterNumber.Create(_schema.Number);
		if (result.IsFailure)
		{
			AppendErrorText(result.Error);
			return false;
		}
		return true;
	}
}
