using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Errors;

public sealed class GroupHasStudentAlreadyError : Error
{
	public GroupHasStudentAlreadyError() => error = "Группа уже содержит такого студента";
	public override string ToString() => error;
}
