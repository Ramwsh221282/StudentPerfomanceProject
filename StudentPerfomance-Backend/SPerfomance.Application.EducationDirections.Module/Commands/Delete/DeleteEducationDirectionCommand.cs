using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Commands.Delete;

internal sealed class DeleteEducationDirectionCommand(IRepositoryExpression<EducationDirection> findDirection) : ICommand
{
	public IRepositoryExpression<EducationDirection> FindDirection { get; init; } = findDirection;
}
