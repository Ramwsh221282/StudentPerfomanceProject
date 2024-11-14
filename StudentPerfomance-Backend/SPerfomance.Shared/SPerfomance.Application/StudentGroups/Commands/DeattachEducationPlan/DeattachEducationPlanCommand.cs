using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Commands.DeattachEducationPlan;

public class DeattachEducationPlanCommand(StudentGroup? group) : ICommand<StudentGroup>
{
    public StudentGroup? Group { get; init; } = group;
}
