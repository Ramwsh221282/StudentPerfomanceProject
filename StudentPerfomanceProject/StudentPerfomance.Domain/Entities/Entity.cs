namespace StudentPerfomance.Domain.Entities;

public abstract class Entity
{
	public Guid Id { get; }
	public Entity(Guid id) => Id = id;
}
