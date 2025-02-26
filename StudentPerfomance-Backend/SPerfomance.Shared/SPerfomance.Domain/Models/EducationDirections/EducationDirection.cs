using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.EducationDirections.ValueObjects;
using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Models.EducationPlans.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.EducationDirections;

public class EducationDirection : DomainEntity
{
    private readonly List<EducationPlan> _plans = [];

    private EducationDirection()
        : base(Guid.Empty)
    {
        Code = DirectionCode.Empty;
        Name = DirectionName.Empty;
        Type = DirectionType.Empty;
    }

    private EducationDirection(DirectionCode code, DirectionName name, DirectionType type)
        : base(Guid.NewGuid())
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

    public static Result<EducationDirection> Create(string? code, string? name, string? type)
    {
        var requestedCode = DirectionCode.Create(code);
        if (requestedCode.IsFailure)
            return requestedCode.Error;

        var requestedName = DirectionName.Create(name);
        if (requestedName.IsFailure)
            return requestedName.Error;

        var requestedType = DirectionType.Create(type);
        if (requestedType.IsFailure)
            return requestedType.Error;

        return new EducationDirection(
            requestedCode.Value,
            requestedName.Value,
            requestedType.Value
        );
    }

    public Result<EducationDirection> ChangeCode(string code)
    {
        var newCode = DirectionCode.Create(code);
        if (newCode.IsFailure)
            return Result<EducationDirection>.Failure(newCode.Error);

        if (Code == newCode.Value)
            return Result<EducationDirection>.Success(this);

        Code = newCode.Value;
        return Result<EducationDirection>.Success(this);
    }

    public Result<EducationDirection> ChangeName(string name)
    {
        var newName = DirectionName.Create(name);
        if (newName.IsFailure)
            return Result<EducationDirection>.Failure(newName.Error);

        if (Name == newName.Value)
            return Result<EducationDirection>.Success(this);

        Name = newName.Value;
        return Result<EducationDirection>.Success(this);
    }

    public Result<EducationDirection> ChangeType(string type)
    {
        var newType = DirectionType.Create(type);
        if (newType.IsFailure)
            return Result<EducationDirection>.Failure(newType.Error);

        if (Type == newType.Value)
            return Result<EducationDirection>.Success(this);

        Type = newType.Value;
        return Result<EducationDirection>.Success(this);
    }

    public Result<EducationPlan> GetEducationPlan(int planYear)
    {
        var plan = _plans.FirstOrDefault(p => p.Year.Year == planYear);
        return plan == null
            ? Result<EducationPlan>.Failure(EducationPlanErrors.NotFoundError())
            : Result<EducationPlan>.Success(plan);
    }

    public Result<EducationPlan> AddEducationPlan(int planYear)
    {
        if (_plans.Any(p => p.Year.Year == planYear))
            return Result<EducationPlan>.Failure(
                EducationPlanErrors.DublicateError(this, planYear)
            );

        var plan = EducationPlan.Create(planYear, this);
        return plan;
    }
}
