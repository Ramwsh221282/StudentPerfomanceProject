using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Errors;

public sealed class GroupPlanAttachmentError : Error
{
	public GroupPlanAttachmentError() => error = $"У группы не закреплен учебный план";
	public override string ToString() => error;
}
