using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Commands.Delete;

public sealed class StudentGroupDeletionService
(
	IRepositoryExpression<StudentGroup> expression,
	IRepository<StudentGroup> repository
) : IService<StudentGroup>
{
	private readonly StudentGroupDeletionCommand _command = new StudentGroupDeletionCommand(expression);
	private readonly StudentGroupDeletionCommandHandler _handler = new StudentGroupDeletionCommandHandler(repository);
	public async Task<OperationResult<StudentGroup>> DoOperation() => await _handler.Handle(_command);
}
