using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.ValueObjects;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections.Validators;

namespace SPerfomance.Domain.Module.Shared.Entities.EducationDirections.ValueObjects;

// Объект значения - направление подготовки.
public sealed class DirectionCode : ValueObject
{
	// Конструктор для EF Core конфигурирования.
	// Инициализирует неверное значение по умолчанию. Не проходит валидатором.
	private DirectionCode() { Code = string.Empty; }

	// Конструктор для создания объекта.
	private DirectionCode(string code) => Code = code;

	// Поле - код направления подготовки.
	public string Code { get; } = null!;

	// Фабричный метод создания объекта со встроенным валидатором.
	public static CSharpFunctionalExtensions.Result<DirectionCode> Create(string code)
	{
		DirectionCode result = new DirectionCode(code);
		Validator<DirectionCode> validator = new DirectionCodeValidator(result);
		return validator.Validate() ? result : CSharpFunctionalExtensions.Result.Failure<DirectionCode>(validator.GetErrorText());
	}

	// Создание дефолтного значения объекта.
	public static DirectionCode CreateDefault() => new DirectionCode();

	// Имплементация интерфейса из ValueObject.
	// Необходим для сравнения двух Value Object.
	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return Code;
	}

	public override string ToString() => Code;
}
