namespace SPerfomance.Domain.Abstractions;

// определение абстрактной сущности
public abstract class DomainEntity
{
	// ИД сущности
	public Guid Id { get; init; }

	public int EntityNumber { get; private set; }

	// конструктор создания объекта абстрактной сущности
	public DomainEntity(Guid id) => Id = id;

	public void SetNumber(int number) => EntityNumber = number;
}
