using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Errors;

public sealed class GroupPlanAlreadyAttachedError : Error
{
	public GroupPlanAlreadyAttachedError() => error = $"У группы уже закреплен учебный план";
	public override string ToString() => error;
}
