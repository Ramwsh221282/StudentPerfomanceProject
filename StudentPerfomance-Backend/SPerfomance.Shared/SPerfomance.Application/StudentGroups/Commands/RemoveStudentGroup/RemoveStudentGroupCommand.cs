using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Commands.RemoveStudentGroup;

public class RemoveStudentGroupCommand(StudentGroup? group) : ICommand<StudentGroup>
{
	public StudentGroup? Group { get; init; } = group;
}
