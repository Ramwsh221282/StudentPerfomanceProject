using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Application.Shared.Module.Schemas.Students;
using SPerfomance.Application.Students.Module.Repository;
using SPerfomance.Application.Students.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.Students;

namespace SPerfomance.Application.Students.Module.Commands.Remove;

internal sealed class RemoveCommand : ICommand
{
	private readonly IRepositoryExpression<Student> _getStudent;
	private readonly IRepositoryExpression<StudentGroup> _getGroup;
	private readonly StudentCommandRepository _repository;
	public readonly ICommandHandler<RemoveCommand, Student> Handler;
	public RemoveCommand(StudentSchema student)
	{
		_getStudent = ExpressionsFactory.GetStudent(student.ToRepositoryObject());
		_getGroup = ExpressionsFactory.GetGroupByName(student.ToRepositoryObject());
		_repository = new StudentCommandRepository();
		Handler = new CommandHandler(_repository);
	}

	internal sealed class CommandHandler(StudentCommandRepository repository) : ICommandHandler<RemoveCommand, Student>
	{
		private readonly StudentCommandRepository _repository = repository;

		public async Task<OperationResult<Student>> Handle(RemoveCommand command)
		{
			Result<Student> delete = await _repository.Remove(command._getStudent, command._getGroup);
			return delete.IsFailure ?
				new OperationResult<Student>(delete.Error) :
				new OperationResult<Student>(delete.Value);
		}
	}
}
