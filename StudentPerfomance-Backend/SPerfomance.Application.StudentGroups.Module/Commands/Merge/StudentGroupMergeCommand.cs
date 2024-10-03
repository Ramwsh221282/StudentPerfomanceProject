using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Errors;

namespace SPerfomance.Application.StudentGroups.Module.Commands.Merge;

public sealed class StudentGroupMergeCommand
(
	IRepositoryExpression<StudentGroup> findGroupA,
	IRepositoryExpression<StudentGroup> findGroupB,
	IRepository<StudentGroup> repository
) : ICommand
{
	private readonly IRepositoryExpression<StudentGroup> _findGroupA = findGroupA;
	private readonly IRepositoryExpression<StudentGroup> _findGroupB = findGroupB;
	public ICommandHandler<StudentGroupMergeCommand, StudentGroup> Handler { get; init; } = new CommandHandler(repository);
	internal sealed class CommandHandler(IRepository<StudentGroup> repository) : ICommandHandler<StudentGroupMergeCommand, StudentGroup>
	{
		private readonly IRepository<StudentGroup> _repository = repository;
		public async Task<OperationResult<StudentGroup>> Handle(StudentGroupMergeCommand command)
		{
			StudentGroup? groupA = await _repository.GetByParameter(command._findGroupA);
			if (groupA == null) return new OperationResult<StudentGroup>(new GroupMergeError().ToString());
			StudentGroup? groupB = await _repository.GetByParameter(command._findGroupB);
			if (groupB == null) return new OperationResult<StudentGroup>(new GroupMergeError().ToString());
			groupA.MergeWithGroup(groupB);
			await _repository.Commit();
			return new OperationResult<StudentGroup>(groupA);
		}
	}
}
