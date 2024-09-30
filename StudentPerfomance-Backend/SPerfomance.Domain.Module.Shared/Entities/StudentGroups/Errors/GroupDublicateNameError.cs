using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Errors;

public class GroupDublicateNameError : Error
{
	public GroupDublicateNameError(string name) => error = $"Группа с названием ${name} уже существует";
	public override string ToString() => error;
}
