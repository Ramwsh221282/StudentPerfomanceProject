using CSharpFunctionalExtensions;

using StudentPerfomance.Application.EntitySchemas.Schemas;
using StudentPerfomance.Application.EntitySchemas.Schemas.Discplines;
using StudentPerfomance.Domain.Entities;

namespace StudentPerfomance.Application.EntitySchemas.Validators.Disciplines;

public sealed class DisciplineValidation(DisciplineSchema schema) : BaseSchemaValidation, ISchemaValidation<DisciplineSchema>
{
	private readonly DisciplineSchema _schema = schema;
	public string Error => errorBuilder.ToString();
	public Func<EntitySchema, bool> BuildCriteria(DisciplineSchema schema) => (schema) => Validate();

	protected override bool Validate()
	{
		Result<Discipline> result = Discipline.Create(Guid.NewGuid(), _schema.Name);
		if (result.IsFailure)
		{
			AppendErrorText(result.Error);
			return false;
		}
		return true;
	}
}
