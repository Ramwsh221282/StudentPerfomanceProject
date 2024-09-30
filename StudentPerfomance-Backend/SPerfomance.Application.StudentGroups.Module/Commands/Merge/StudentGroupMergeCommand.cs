using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Commands.Merge;

internal sealed class StudentGroupMergeCommand
(
	IRepositoryExpression<StudentGroup> findGroupA,
	IRepositoryExpression<StudentGroup> findGroupB
) : ICommand
{
	public IRepositoryExpression<StudentGroup> FindGroupA { get; init; } = findGroupA;
	public IRepositoryExpression<StudentGroup> FindGroupB { get; init; } = findGroupB;
}
