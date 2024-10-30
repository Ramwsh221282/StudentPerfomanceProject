namespace SPerfomance.Domain.Abstractions;

public interface ISpecification<TEntity> where TEntity : DomainEntity
{
	public string Error { get; }

	public bool IsSatisfied(TEntity? entity);
}
