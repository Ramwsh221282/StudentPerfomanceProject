using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Commands.CreateEducationDirection;

public class CreateEducationDirectionCommand(string name, string code, string type)
    : ICommand<EducationDirection>
{
    public string Name { get; init; } = name;

    public string Type { get; init; } = type;

    public string Code { get; init; } = code;
}
