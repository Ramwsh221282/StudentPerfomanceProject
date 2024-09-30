using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Errors;

public sealed class GroupMergeError : Error
{
	public GroupMergeError() => error = "Ошибка в смешивании групп. Не найдена одна из групп";
	public override string ToString() => error;
}
