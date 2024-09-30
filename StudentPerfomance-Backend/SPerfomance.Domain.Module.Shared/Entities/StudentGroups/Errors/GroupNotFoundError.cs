using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Errors;

public sealed class GroupNotFoundError : Error
{
	public GroupNotFoundError() => error = "Группа не найдена";
	public override string ToString() => error;
}
