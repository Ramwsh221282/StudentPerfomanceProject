namespace SPerfomance.Domain.Abstractions;

public abstract class AggregateRoot : DomainEntity
{
	protected AggregateRoot(Guid id) : base(id) { }
}
