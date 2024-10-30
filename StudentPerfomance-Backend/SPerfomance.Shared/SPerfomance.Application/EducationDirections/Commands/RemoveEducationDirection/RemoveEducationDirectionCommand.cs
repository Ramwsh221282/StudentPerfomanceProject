using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Commands.RemoveEducationDirection;

public class RemoveEducationDirectionCommand(EducationDirection? direction) : ICommand<EducationDirection>
{
	public EducationDirection? Direction { get; init; } = direction;
}
