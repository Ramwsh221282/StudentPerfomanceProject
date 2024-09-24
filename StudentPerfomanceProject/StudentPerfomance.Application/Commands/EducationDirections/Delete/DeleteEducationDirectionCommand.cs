using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.EducationDirections.Delete;

internal sealed class DeleteEducationDirectionCommand(IRepositoryExpression<EducationDirection> findDirection) : ICommand
{
	public IRepositoryExpression<EducationDirection> FindDirection { get; init; } = findDirection;
}
