using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Application.StudentGroups.Module.Repository;
using SPerfomance.Application.StudentGroups.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Commands.Delete;

internal sealed class DeleteCommand : ICommand
{
	private readonly IRepositoryExpression<StudentGroup> _expression;
	private readonly StudentGroupCommandRepository _repository;
	public ICommandHandler<DeleteCommand, StudentGroup> Handler;

	public DeleteCommand(StudentsGroupSchema group)
	{
		_expression = ExpressionsFactory.GetByName(group.ToRepositoryObject());
		_repository = new StudentGroupCommandRepository();
		Handler = new CommandHandler(_repository);
	}

	internal sealed class CommandHandler(StudentGroupCommandRepository repository) : ICommandHandler<DeleteCommand, StudentGroup>
	{
		private readonly StudentGroupCommandRepository _repository = repository;
		public async Task<OperationResult<StudentGroup>> Handle(DeleteCommand command)
		{
			Result<StudentGroup> delete = await _repository.Remove(command._expression);
			return delete.IsFailure ?
				new OperationResult<StudentGroup>(delete.Error) :
				new OperationResult<StudentGroup>(delete.Value);
		}
	}
}
