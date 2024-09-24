using CSharpFunctionalExtensions;

namespace StudentPerfomance.Domain.ValueObjects.StudentGroup;

public sealed class GroupBachelorDirection : GroupEducationDirection
{
	private GroupBachelorDirection()
	{
		_direction = "Бакалавриат";
	}

	public static Result<GroupEducationDirection> Create() => new GroupBachelorDirection();
}
