using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Commands.Merge;

public sealed class StudentGroupsMergeService
(
	IRepositoryExpression<StudentGroup> findGroupA,
	IRepositoryExpression<StudentGroup> findGroupB,
	IRepository<StudentGroup> repository
) : IService<StudentGroup>
{
	private readonly StudentGroupMergeCommand _command = new StudentGroupMergeCommand(findGroupA, findGroupB);
	private readonly StudentGroupMergeCommandHandler _handler = new StudentGroupMergeCommandHandler(repository);
	public async Task<OperationResult<StudentGroup>> DoOperation() => await _handler.Handle(_command);
}
