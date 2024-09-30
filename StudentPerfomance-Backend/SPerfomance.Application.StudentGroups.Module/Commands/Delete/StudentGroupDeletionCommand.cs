using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Commands.Delete;

internal sealed class StudentGroupDeletionCommand(IRepositoryExpression<StudentGroup> expression) : ICommand
{
	public IRepositoryExpression<StudentGroup> Expression { get; init; } = expression;
}
