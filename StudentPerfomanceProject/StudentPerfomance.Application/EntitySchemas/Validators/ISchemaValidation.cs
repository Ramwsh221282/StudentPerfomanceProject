using StudentPerfomance.Application.EntitySchemas.Schemas;

namespace StudentPerfomance.Application.EntitySchemas.Validators;

public interface ISchemaValidation<TSchema> where TSchema : EntitySchema
{
	public string Error { get; }
	public Func<EntitySchema, bool> BuildCriteria(TSchema schema);
}
