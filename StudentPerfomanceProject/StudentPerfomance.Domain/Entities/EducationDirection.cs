using CSharpFunctionalExtensions;

using StudentPerfomance.Domain.Validators;
using StudentPerfomance.Domain.ValueObjects.EducationDirections;

namespace StudentPerfomance.Domain.Entities;

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

	// Метод изменения направления подготовки.
	public void ChangeDirectionName(DirectionName name) => Name = name;
	// Фабричный метод создания объекта.
	public static Result<EducationDirection> Create(DirectionCode code, DirectionName name, DirectionType type)
	{
		EducationDirection direction = new EducationDirection(Guid.NewGuid(), code, name, type);
		Validator<EducationDirection> validator = new EducationDirectionValidator(direction);
		return validator.Validate() ? direction : Result.Failure<EducationDirection>(validator.GetErrorText());
	}
	// Создание дефолтного объекта.
	public static EducationDirection CreateDefault() => new EducationDirection();
}
