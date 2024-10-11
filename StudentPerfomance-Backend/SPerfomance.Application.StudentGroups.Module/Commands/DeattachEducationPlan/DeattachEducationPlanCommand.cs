using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Application.StudentGroups.Module.Repository;
using SPerfomance.Application.StudentGroups.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Errors;

namespace SPerfomance.Application.StudentGroups.Module.Commands.DeattachEducationPlan;

internal sealed class DeattachEducationPlanCommand : ICommand
{
	private readonly IRepositoryExpression<StudentGroup> _getGroup;
	private readonly StudentGroupQueryRepository _repository = new StudentGroupQueryRepository();

	public readonly ICommandHandler<DeattachEducationPlanCommand, StudentGroup> Handler;

	public DeattachEducationPlanCommand(StudentsGroupSchema group)
	{
		_getGroup = ExpressionsFactory.GetByName(group.ToRepositoryObject());
		_repository = new StudentGroupQueryRepository();
		Handler = new CommandHandler(_repository);
	}

	internal sealed class CommandHandler(StudentGroupQueryRepository repository) : ICommandHandler<DeattachEducationPlanCommand, StudentGroup>
	{
		private readonly StudentGroupQueryRepository _repository = repository;

		public async Task<OperationResult<StudentGroup>> Handle(DeattachEducationPlanCommand command)
		{
			StudentGroup? group = await _repository.GetByParameter(command._getGroup);
			if (group == null)
				return new OperationResult<StudentGroup>(new GroupNotFoundError().ToString());

			Result deattach = group.DeattachEducationPlan();
			if (deattach.IsFailure)
				return new OperationResult<StudentGroup>(deattach.Error);

			await _repository.Commit();
			return new OperationResult<StudentGroup>(group);
		}
	}
}
