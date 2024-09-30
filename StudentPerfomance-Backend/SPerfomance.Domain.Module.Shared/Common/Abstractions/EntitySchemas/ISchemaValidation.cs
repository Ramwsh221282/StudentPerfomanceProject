namespace SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;

public interface ISchemaValidation<TSchema> where TSchema : EntitySchema
{
	public string Error { get; }
	public Func<EntitySchema, bool> BuildCriteria(TSchema schema);
}
