namespace SPerfomance.Application.Shared.Module.Schemas;

public interface ISchemaValidation<TSchema> where TSchema : EntitySchema
{
	public string Error { get; }
	public Func<EntitySchema, bool> BuildCriteria(TSchema schema);
}
