using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Errors;

namespace SPerfomance.Application.StudentGroups.Module.Commands.Merge;

internal sealed class StudentGroupMergeCommandHandler
(
	IRepository<StudentGroup> repository
) : ICommandHandler<StudentGroupMergeCommand, OperationResult<StudentGroup>>
{
	private readonly IRepository<StudentGroup> _repository = repository;
	public async Task<OperationResult<StudentGroup>> Handle(StudentGroupMergeCommand command)
	{
		StudentGroup? groupA = await _repository.GetByParameter(command.FindGroupA);
		if (groupA == null) return new OperationResult<StudentGroup>(new GroupMergeError().ToString());
		StudentGroup? groupB = await _repository.GetByParameter(command.FindGroupB);
		if (groupB == null) return new OperationResult<StudentGroup>(new GroupMergeError().ToString());
		groupA.MergeWithGroup(groupB);
		await _repository.Commit();
		return new OperationResult<StudentGroup>(groupA);
	}
}
