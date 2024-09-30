using SPerfomance.Domain.Module.Shared.Common.Abstractions.Entities;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans.Validators;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans.ValueObjects;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;

namespace SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

// Сущность. Учебный план.
public sealed class EducationPlan : Entity
{
	// Список семестров.
	private List<Semester> _semesters = [];
	// Конструктор создания дефолтного объекта.
	private EducationPlan() : base(Guid.Empty)
	{
		Year = YearOfCreation.CreateDefault();
		Direction = EducationDirection.CreateDefault();
	}
	// Конструктор создания объекта без семестров.
	private EducationPlan(Guid id, EducationDirection direction, YearOfCreation year) : base(id)
	{
		Direction = direction;
		Year = year;
	}

	// Направление подготовки.
	public EducationDirection Direction { get; private set; } = null!;
	// Год набора.
	public YearOfCreation Year { get; private set; } = null!;
	// Получение списка семестров только для чтения.
	public IReadOnlyCollection<Semester> Semesters => _semesters;
	// Добавление семестра
	public void AddSemester(Semester semester) => _semesters.Add(semester);
	// Фабричный метод создания дефолтного объекта.
	public static EducationPlan CreateDefault() => new EducationPlan();
	// Фабричный метод создания учебного плана без семестров.
	public static CSharpFunctionalExtensions.Result<EducationPlan> Create(EducationDirection direction, YearOfCreation year)
	{
		EducationPlan plan = new EducationPlan(Guid.NewGuid(), direction, year);
		Validator<YearOfCreation> validator = new YearOfCreationValidator(year);
		return validator.Validate() ? plan : CSharpFunctionalExtensions.Result.Failure<EducationPlan>(validator.GetErrorText());
	}

	public override bool Equals(object? obj)
	{
		if (obj == null) return false;
		if (obj is EducationPlan other)
		{
			return other.Year == Year &&
				   other.Direction.Name == Direction.Name &&
				   other.Direction.Code == Direction.Code &&
				   other.Direction.Type == Direction.Type;
		}
		return false;
	}

	public override int GetHashCode() => GetHashCode();
}
