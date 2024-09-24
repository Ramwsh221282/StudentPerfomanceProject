using CSharpFunctionalExtensions;
using StudentPerfomance.Domain.Validators;
using StudentPerfomance.Domain.Validators.StudentGroup;

namespace StudentPerfomance.Domain.ValueObjects.StudentGroup;

public class GroupEducationDirection : ValueObject
{
	protected string _direction;
	protected GroupEducationDirection() { }
	protected GroupEducationDirection(string direction) => _direction = direction;
	public string Direction => _direction;
	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return _direction;
	}

	public static Result<GroupEducationDirection> Create(string name)
	{
		GroupEducationDirection direction = new GroupEducationDirection(name);
		Validator<GroupEducationDirection> validator = new GroupEducationDirectionValidator(direction);
		return validator.Validate() == true ? direction : Result.Failure<GroupEducationDirection>(validator.GetErrorText());
	}
}
