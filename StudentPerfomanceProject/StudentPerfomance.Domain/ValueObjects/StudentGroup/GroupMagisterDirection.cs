using CSharpFunctionalExtensions;

namespace StudentPerfomance.Domain.ValueObjects.StudentGroup;

public sealed class GroupMagisterDirection : GroupEducationDirection
{
	private GroupMagisterDirection()
	{
		_direction = "Магистратура";
	}

	public static Result<GroupEducationDirection> Create() => new GroupMagisterDirection();
}
