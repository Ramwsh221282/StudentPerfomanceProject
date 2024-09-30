using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.ValueObjects;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans.Validators;

namespace SPerfomance.Domain.Module.Shared.Entities.EducationPlans.ValueObjects;

// Объект значения. Год набора.
public sealed class YearOfCreation : ValueObject
{
	// Пустой конструктор для конфигурирования EF Core.
	private YearOfCreation() { }
	// Конструктор для создания объекта.
	private YearOfCreation(uint year) => Year = year;
	// Значение объекта.
	public uint Year { get; }
	// Фабричный метод создания объекта со встроенной валидацией.
	public static CSharpFunctionalExtensions.Result<YearOfCreation> Create(uint year)
	{
		YearOfCreation result = new YearOfCreation(year);
		Validator<YearOfCreation> validator = new YearOfCreationValidator(result);
		return validator.Validate() ? result : CSharpFunctionalExtensions.Result.Failure<YearOfCreation>(validator.GetErrorText());
	}
	// Создания дефолтного объекта.
	public static YearOfCreation CreateDefault() => new YearOfCreation();
	// Имплементация Value Object для сравнения.
	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return Year;
	}

	public override string ToString() => Year.ToString();
}
