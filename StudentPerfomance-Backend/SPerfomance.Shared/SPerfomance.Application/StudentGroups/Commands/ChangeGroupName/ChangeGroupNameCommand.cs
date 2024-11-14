using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Commands.ChangeGroupName;

public class ChangeGroupNameCommand(StudentGroup? group, string newName) : ICommand<StudentGroup>
{
    public StudentGroup? Group { get; init; } = group;

    public string NewName = newName;
}
