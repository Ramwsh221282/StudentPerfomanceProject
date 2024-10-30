using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.EducationDirections.Commands.UpdateEducationDirection;

public class UpdateEducationDirectionCommand
(
	EducationDirection? direction,
	string? newName,
	string? newCode,
	string? newType
)
 : ICommand<EducationDirection>
{
	public EducationDirection? Direction { get; init; } = direction;

	public string NewName { get; init; } = newName.ValueOrEmpty();

	public string NewCode { get; init; } = newCode.ValueOrEmpty();

	public string NewType { get; init; } = newType.ValueOrEmpty();
}
