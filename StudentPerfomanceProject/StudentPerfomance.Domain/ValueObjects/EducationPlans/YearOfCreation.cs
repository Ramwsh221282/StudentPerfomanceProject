using CSharpFunctionalExtensions;

using StudentPerfomance.Domain.Validators;
using StudentPerfomance.Domain.Validators.EducationPlans;

namespace StudentPerfomance.Domain.ValueObjects.EducationPlans;

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
	public static Result<YearOfCreation> Create(uint year)
	{
		YearOfCreation result = new YearOfCreation(year);
		Validator<YearOfCreation> validator = new YearOfCreationValidator(result);
		return validator.Validate() ? result : Result.Failure<YearOfCreation>(validator.GetErrorText());
	}
	// Создания дефолтного объекта.
	public static YearOfCreation CreateDefault() => new YearOfCreation();
	// Имплементация Value Object для сравнения.
	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return Year;
	}
}
