using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Errors;

public sealed class GroupSameNameError : Error
{
	public GroupSameNameError() => error = "Группа уже имеет такое название";
	public override string ToString() => error;
}
