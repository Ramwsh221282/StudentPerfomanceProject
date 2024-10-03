using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Errors;

namespace SPerfomance.Application.StudentGroups.Module.Commands.Delete;

public sealed class StudentGroupDeletionCommand(IRepositoryExpression<StudentGroup> expression, IRepository<StudentGroup> repository) : ICommand
{
	private readonly IRepositoryExpression<StudentGroup> _expression = expression;
	public ICommandHandler<StudentGroupDeletionCommand, StudentGroup> Handler { get; init; } = new CommandHandler(repository);
	internal sealed class CommandHandler(IRepository<StudentGroup> repository) : ICommandHandler<StudentGroupDeletionCommand, StudentGroup>
	{
		private readonly IRepository<StudentGroup> _repository = repository;
		public async Task<OperationResult<StudentGroup>> Handle(StudentGroupDeletionCommand command)
		{
			StudentGroup? group = await _repository.GetByParameter(command._expression);
			if (group == null) return new OperationResult<StudentGroup>(new GroupNotFoundError().ToString());
			await _repository.Remove(group);
			return new OperationResult<StudentGroup>(group);
		}
	}
}
