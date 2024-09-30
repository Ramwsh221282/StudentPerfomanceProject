using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Errors;

namespace SPerfomance.Application.StudentGroups.Module.Commands.Delete;

internal sealed class StudentGroupDeletionCommandHandler
(
	IRepository<StudentGroup> repository
) : ICommandHandler<StudentGroupDeletionCommand, OperationResult<StudentGroup>>
{
	private readonly IRepository<StudentGroup> _repository = repository;

	public async Task<OperationResult<StudentGroup>> Handle(StudentGroupDeletionCommand command)
	{
		StudentGroup? group = await _repository.GetByParameter(command.Expression);
		if (group == null) return new OperationResult<StudentGroup>(new GroupNotFoundError().ToString());
		await _repository.Remove(group);
		return new OperationResult<StudentGroup>(group);
	}
}
