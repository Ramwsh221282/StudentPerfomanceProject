using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Application.Shared.Users.Module.Commands.Common;
using SPerfomance.Application.StudentGroups.Module.Repository;
using SPerfomance.Application.StudentGroups.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.StudentGroups.Module.Commands.Delete;

internal sealed class DeleteCommand : ICommand
{
	private readonly IRepositoryExpression<StudentGroup> _expression;
	private readonly StudentGroupCommandRepository _repository;
	public ICommandHandler<DeleteCommand, StudentGroup> Handler;

	public DeleteCommand(StudentsGroupSchema group, string token)
	{
		_expression = ExpressionsFactory.GetByName(group.ToRepositoryObject());
		_repository = new StudentGroupCommandRepository();
		Handler = new VerificationHandler<DeleteCommand, StudentGroup>(token, User.Admin);
		Handler = new CommandHandler(Handler, _repository);
	}

	internal sealed class CommandHandler : DecoratedCommandHandler<DeleteCommand, StudentGroup>
	{
		private readonly StudentGroupCommandRepository _repository;

		public CommandHandler(
			ICommandHandler<DeleteCommand, StudentGroup> handler,
			StudentGroupCommandRepository repository)
			 : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<StudentGroup>> Handle(DeleteCommand command)
		{
			var result = await base.Handle(command);
			if (result.IsFailed)
				return result;

			Result<StudentGroup> delete = await _repository.Remove(command._expression);
			return delete.IsFailure ?
				new OperationResult<StudentGroup>(delete.Error) :
				new OperationResult<StudentGroup>(delete.Value);
		}
	}
}
