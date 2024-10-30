using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Commands.MergeWithGroup;

public class MergeWithGroupCommand(StudentGroup initial, StudentGroup target) : ICommand<StudentGroup>
{
	public StudentGroup Initial { get; init; } = initial;

	public StudentGroup Target { get; init; } = target;
}
