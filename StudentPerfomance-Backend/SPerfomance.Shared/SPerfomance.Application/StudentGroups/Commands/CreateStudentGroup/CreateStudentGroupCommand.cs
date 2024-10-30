using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.StudentGroups.Commands.CreateStudentGroup;

public class CreateStudentGroupCommand(string? name) : ICommand<StudentGroup>
{
	public string Name { get; init; } = name.ValueOrEmpty();
}
