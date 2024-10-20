using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Application.Shared.Module.Schemas.Students;
using SPerfomance.Application.Shared.Users.Module.Commands.Common;
using SPerfomance.Application.Students.Module.Repository;
using SPerfomance.Application.Students.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.Students;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.Students.Module.Commands.Remove;

internal sealed class RemoveCommand : ICommand
{
	private readonly IRepositoryExpression<Student> _getStudent;
	private readonly IRepositoryExpression<StudentGroup> _getGroup;
	private readonly StudentCommandRepository _repository;

	public readonly ICommandHandler<RemoveCommand, Student> Handler;

	public RemoveCommand(StudentSchema student, string token)
	{
		_getStudent = ExpressionsFactory.GetStudent(student.ToRepositoryObject());
		_getGroup = ExpressionsFactory.GetGroupByName(student.ToRepositoryObject());
		_repository = new StudentCommandRepository();
		Handler = new VerificationHandler<RemoveCommand, Student>(token, User.Admin);
		Handler = new CommandHandler(Handler, _repository);
	}

	internal sealed class CommandHandler : DecoratedCommandHandler<RemoveCommand, Student>
	{
		private readonly StudentCommandRepository _repository;

		public CommandHandler(
			ICommandHandler<RemoveCommand, Student> handler,
			StudentCommandRepository repository) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<Student>> Handle(RemoveCommand command)
		{
			var result = await base.Handle(command);
			if (result.IsFailed)
				return result;

			Result<Student> delete = await _repository.Remove(command._getStudent, command._getGroup);
			return delete.IsFailure ?
				new OperationResult<Student>(delete.Error) :
				new OperationResult<Student>(delete.Value);
		}
	}
}
