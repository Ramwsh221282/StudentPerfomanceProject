using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Errors;

public sealed class GroupNameLengthError : Error
{
	public GroupNameLengthError(int maxLength) => error = $"Название группы превышает {maxLength} символов";
	public override string ToString() => error;
}
