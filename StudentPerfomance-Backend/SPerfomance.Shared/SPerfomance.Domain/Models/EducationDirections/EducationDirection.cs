using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.EducationDirections.ValueObjects;
using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Models.EducationPlans.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.EducationDirections;

public class EducationDirection : DomainEntity
{
	private readonly List<EducationPlan> _plans = [];

	private EducationDirection() : base(Guid.Empty)
	{
		Code = DirectionCode.Empty;
		Name = DirectionName.Empty;
		Type = DirectionType.Empty;
	}

	private EducationDirection(DirectionCode code, DirectionName name, DirectionType type) : base(Guid.NewGuid())
	{
		Code = code;
		Name = name;
		Type = type;
	}

	public DirectionCode Code { get; private set; }

	public DirectionName Name { get; private set; }

	public DirectionType Type { get; private set; }

	public IReadOnlyCollection<EducationPlan> Plans => _plans;

	public static EducationDirection Empty => new EducationDirection();

	public static Result<EducationDirection> Create(string code, string name, string type)
	{
		Result<DirectionCode> requestedCode = DirectionCode.Create(code);
		if (requestedCode.IsFailure)
			return Result<EducationDirection>.Failure(requestedCode.Error);

		Result<DirectionName> requestedName = DirectionName.Create(name);
		if (requestedName.IsFailure)
			return Result<EducationDirection>.Failure(requestedName.Error);

		Result<DirectionType> requestedType = DirectionType.Create(type);
		if (requestedType.IsFailure)
			return Result<EducationDirection>.Failure(requestedType.Error);

		EducationDirection direction = new EducationDirection(
			requestedCode.Value,
			requestedName.Value,
			requestedType.Value
		);

		return Result<EducationDirection>.Success(direction);
	}

	public Result<EducationDirection> ChangeCode(string code)
	{
		Result<DirectionCode> newCode = DirectionCode.Create(code);
		if (newCode.IsFailure)
			return Result<EducationDirection>.Failure(newCode.Error);

		if (Code == newCode.Value)
			return Result<EducationDirection>.Success(this);

		Code = newCode.Value;
		return Result<EducationDirection>.Success(this);
	}

	public Result<EducationDirection> ChangeName(string name)
	{
		Result<DirectionName> newName = DirectionName.Create(name);
		if (newName.IsFailure)
			return Result<EducationDirection>.Failure(newName.Error);

		if (Name == newName.Value)
			return Result<EducationDirection>.Success(this);

		Name = newName.Value;
		return Result<EducationDirection>.Success(this);
	}

	public Result<EducationDirection> ChangeType(string type)
	{
		Result<DirectionType> newType = DirectionType.Create(type);
		if (newType.IsFailure)
			return Result<EducationDirection>.Failure(newType.Error);

		if (Type == newType.Value)
			return Result<EducationDirection>.Success(this);

		Type = newType.Value;
		return Result<EducationDirection>.Success(this);
	}

	public Result<EducationPlan> GetEducationPlan(int planYear)
	{
		EducationPlan? plan = _plans.FirstOrDefault(p => p.Year.Year == planYear);
		return plan == null ?
			Result<EducationPlan>.Failure(EducationPlanErrors.NotFoundError()) :
			Result<EducationPlan>.Success(plan);
	}

	public Result<EducationPlan> AddEducationPlan(int planYear)
	{
		if (_plans.Any(p => p.Year.Year == planYear) == true)
			return Result<EducationPlan>.Failure(EducationPlanErrors.DublicateError(this, planYear));

		Result<EducationPlan> plan = EducationPlan.Create(planYear, this);
		return plan;
	}
}
