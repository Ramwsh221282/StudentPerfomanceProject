using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Application.Shared.Users.Module.Commands.Common;
using SPerfomance.Application.Teachers.Module.Repository;
using SPerfomance.Application.Teachers.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.Teachers.Module.Commands.Remove;

internal sealed class RemoveCommand : ICommand
{
	private readonly IRepositoryExpression<Teacher> _getTeacher;
	private readonly TeacherCommandRepository _repository;

	public readonly ICommandHandler<RemoveCommand, Teacher> Handler;

	public RemoveCommand(TeacherSchema teacher, string token)
	{
		_getTeacher = ExpressionsFactory.GetTeacher(teacher.ToRepositoryObject());
		_repository = new TeacherCommandRepository();
		Handler = new VerificationHandler<RemoveCommand, Teacher>(token, User.Admin);
		Handler = new CommandHandler(Handler, _repository);
	}

	internal sealed class CommandHandler : DecoratedCommandHandler<RemoveCommand, Teacher>
	{
		private readonly TeacherCommandRepository _repository;

		public CommandHandler(
			ICommandHandler<RemoveCommand, Teacher> handler,
			TeacherCommandRepository repository) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<Teacher>> Handle(RemoveCommand command)
		{
			var result = await base.Handle(command);
			if (result.IsFailed)
				return result;

			Result<Teacher> delete = await _repository.Remove(command._getTeacher);
			return delete.IsFailure ?
				new OperationResult<Teacher>(delete.Error) :
				new OperationResult<Teacher>(delete.Value);
		}
	}
}
