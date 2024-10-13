using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Application.StudentGroups.Module.Repository;
using SPerfomance.Application.StudentGroups.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Errors;

namespace SPerfomance.Application.StudentGroups.Module.Commands.MergeGroups;

internal sealed class MergeGroupCommand : ICommand
{
	private readonly IRepositoryExpression<StudentGroup> _getInitial;
	private readonly IRepositoryExpression<StudentGroup> _getTarget;
	private readonly StudentGroupQueryRepository _repository;

	public readonly ICommandHandler<MergeGroupCommand, StudentGroup> Handler;

	public MergeGroupCommand(StudentsGroupSchema initial, StudentsGroupSchema target)
	{
		_getInitial = ExpressionsFactory.GetByName(initial.ToRepositoryObject());
		_getTarget = ExpressionsFactory.GetByName(target.ToRepositoryObject());
		_repository = new StudentGroupQueryRepository();
		Handler = new CommandHandler(_repository);
	}

	internal sealed class CommandHandler(StudentGroupQueryRepository repository) : ICommandHandler<MergeGroupCommand, StudentGroup>
	{
		private readonly StudentGroupQueryRepository _repository = repository;

		public async Task<OperationResult<StudentGroup>> Handle(MergeGroupCommand command)
		{
			StudentGroup? initial = await _repository.GetByParameter(command._getInitial);
			if (initial == null)
				return new OperationResult<StudentGroup>(new GroupNotFoundError().ToString());

			StudentGroup? target = await _repository.GetByParameter(command._getTarget);
			if (target == null)
				return new OperationResult<StudentGroup>(new GroupNotFoundError().ToString());

			Result merge = initial.MergeWithGroup(target);
			if (merge.IsFailure)
				return new OperationResult<StudentGroup>(merge.Error);

			await _repository.Commit();
			return new OperationResult<StudentGroup>(initial);
		}
	}
}
