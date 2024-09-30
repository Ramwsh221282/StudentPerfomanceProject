namespace StudentPerfomance.Domain.Errors.StudentGroups;

public sealed class GroupNameError : Error
{
	public GroupNameError(int length) => error = $"Название группы пустое или превышает {length} символов";
}
