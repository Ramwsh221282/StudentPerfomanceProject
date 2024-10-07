namespace SPerfomance.Domain.Module.Shared.Common.Abstractions.Entities;

public abstract class Entity
{
	// ИД сущностии
	public Guid Id { get; private set; }
	// Номер сущности в таблице
	public int EntityNumber { get; private set; }
	// Конструктор сущности
	public Entity(Guid id) => Id = id;
	// Метод присвоения номера сущности
	public Entity SetNumber(int number)
	{
		EntityNumber = number;
		return this;
	}

	protected CSharpFunctionalExtensions.Result Success() => CSharpFunctionalExtensions.Result.Success();
	protected CSharpFunctionalExtensions.Result Failure(string errorMessage) => CSharpFunctionalExtensions.Result.Failure(errorMessage);
}
