using CSharpFunctionalExtensions;
using StudentPerfomance.Domain.Validators;
using StudentPerfomance.Domain.Validators.EducationDIrections;

namespace StudentPerfomance.Domain.ValueObjects.EducationDirections;

// Объект значения. Название направления подготовки.
public sealed class DirectionName : ValueObject
{
	// Пустой конструктор для создания неправильного значения. Валидатором не пройдёт.
	// Необходим для конфигурарования в EF Core.
	private DirectionName() => Name = string.Empty;
	// Конструктор для создания объекта.
	private DirectionName(string name) => Name = name;
	// Поле объекта "Название направления".
	public string Name { get; } = null!;
	// Фабричный метод создания объекта со встроенным валидатором.
	public static Result<DirectionName> Create(string name)
	{
		DirectionName result = new DirectionName(name);
		Validator<DirectionName> validator = new DirectionNameValidator(result);
		return validator.Validate() ? result : Result.Failure<DirectionName>(validator.GetErrorText());
	}
	// Создание дефолтного значения объекта.
	public static DirectionName CreateDefault() => new DirectionName();

	// Имплементация интерфейса из Value Object для сравнения.
	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return Name;
	}

	public override string ToString() => Name;
}
