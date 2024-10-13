using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Errors;

public sealed class GroupMergeConflictError : Error
{
	public GroupMergeConflictError() => error = "Невозможно смешать студентов одной и той же группы";

	public override string ToString() => error;
}
