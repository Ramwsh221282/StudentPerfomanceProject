using SPerfomance.Domain.Module.Shared.Common.Abstractions.Entities;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections.Errors;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections.Validators;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections.ValueObjects;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

// Сущность - "Направление подготовки".
public sealed class EducationDirection : Entity
{
	// Учебные планы по направлению
	private readonly List<EducationPlan> _plans = [];

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

	// Учебные планы read-only.
	public IReadOnlyCollection<EducationPlan> Plans => _plans;

	// Метод изменения имени направления подготовки.
	public CSharpFunctionalExtensions.Result ChangeDirectionName(DirectionName name)
	{
		Validator<DirectionName> validator = new DirectionNameValidator(name);
		if (!validator.Validate())
			return CSharpFunctionalExtensions.Result.Failure(validator.GetErrorText());

		if (Name == name)
			return CSharpFunctionalExtensions.Result.Failure(new DirectionSameNameError(this).ToString());

		Name = name;
		return CSharpFunctionalExtensions.Result.Success();
	}

	// Метод изменения кода направления подготовки
	public CSharpFunctionalExtensions.Result ChangeDirectionCode(DirectionCode code)
	{
		Validator<DirectionCode> validator = new DirectionCodeValidator(code);

		if (!validator.Validate())
			return CSharpFunctionalExtensions.Result.Failure(validator.GetErrorText());

		if (Code == code)
			return CSharpFunctionalExtensions.Result.Failure(new DirectionCodeSameError(this).ToString());

		Code = code;
		return CSharpFunctionalExtensions.Result.Success();
	}

	// Метод добавления учебного плана в коллекцию учебных планов.
	public CSharpFunctionalExtensions.Result<EducationPlan> AddPlan(EducationPlan plan)
	{
		if (plan == null)
			return CSharpFunctionalExtensions.Result.Failure<EducationPlan>(new EducationPlanNotFoundError().ToString());

		if (_plans.Any(p => p.Year == plan.Year))
			return CSharpFunctionalExtensions.Result.Failure<EducationPlan>(new EducationPlanDublicateError().ToString());

		_plans.Add(plan);
		return plan;
	}

	// Фабричный метод создания объекта.
	public static CSharpFunctionalExtensions.Result<EducationDirection> Create(DirectionCode code, DirectionName name, DirectionType type)
	{
		EducationDirection direction = new EducationDirection(Guid.NewGuid(), code, name, type);
		return direction;
	}

	// Создание дефолтного объекта.
	public static EducationDirection CreateDefault() => new EducationDirection();
}
