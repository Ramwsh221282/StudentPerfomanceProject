using SPerfomance.Domain.Module.Shared.Common.Abstractions.Entities;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections.ValueObjects;

namespace SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

// Сущность - "Направление подготовки".
public sealed class EducationDirection : Entity
{
	// Конструктор создания дефолтного объекта
	private EducationDirection() : base(Guid.Empty)
	{
		Code = DirectionCode.CreateDefault();
		Name = DirectionName.CreateDefault();
		Type = DirectionType.CreateDefault();
	}
	private EducationDirection(Guid id, DirectionCode code, DirectionName name, DirectionType type) : base(id)
	{
		Code = code;
		Name = name;
		Type = type;
	}
	// Объект значение - "Код направления подготовки".
	public DirectionCode Code { get; private set; } = null!;
	// Объект значение - "Название направления подготовки".
	public DirectionName Name { get; private set; } = null!;
	// Тип направления подготовки магистратура/бакалавриат;
	public DirectionType Type { get; private set; } = null!;

	// Метод изменения имени направления подготовки.
	public void ChangeDirectionName(DirectionName name) => Name = name;
	// Метод изменения кода направления подготовки
	public void ChangeDirectionCode(DirectionCode code) => Code = code;
	// Фабричный метод создания объекта.
	public static CSharpFunctionalExtensions.Result<EducationDirection> Create(DirectionCode code, DirectionName name, DirectionType type)
	{
		EducationDirection direction = new EducationDirection(Guid.NewGuid(), code, name, type);
		return direction;
	}
	// Создание дефолтного объекта.
	public static EducationDirection CreateDefault() => new EducationDirection();

	public override bool Equals(object? obj)
	{
		if (obj == null) return false;
		if (obj is EducationDirection other)
		{
			return other.Id == Id &&
						   other.EntityNumber == EntityNumber &&
						   other.Name == Name &&
						   other.Code == Code &&
						   other.Type == Type;
		}
		return false;
	}

	public override int GetHashCode() => GetHashCode();
}
