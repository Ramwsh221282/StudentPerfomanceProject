using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Application.Teachers.Module.Repository;
using SPerfomance.Application.Teachers.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Application.Teachers.Module.Commands.Remove;

internal sealed class RemoveCommand : ICommand
{
	private readonly IRepositoryExpression<Teacher> _getTeacher;
	private readonly TeacherCommandRepository _repository;
	public readonly ICommandHandler<RemoveCommand, Teacher> Handler;
	public RemoveCommand(TeacherSchema teacher)
	{
		_getTeacher = ExpressionsFactory.GetTeacher(teacher.ToRepositoryObject());
		_repository = new TeacherCommandRepository();
		Handler = new CommandHandler(_repository);
	}

	internal sealed class CommandHandler(TeacherCommandRepository repository) : ICommandHandler<RemoveCommand, Teacher>
	{
		private readonly TeacherCommandRepository _repository = repository;
		public async Task<OperationResult<Teacher>> Handle(RemoveCommand command)
		{
			Result<Teacher> delete = await _repository.Remove(command._getTeacher);
			return delete.IsFailure ?
				new OperationResult<Teacher>(delete.Error) :
				new OperationResult<Teacher>(delete.Value);
		}
	}
}
