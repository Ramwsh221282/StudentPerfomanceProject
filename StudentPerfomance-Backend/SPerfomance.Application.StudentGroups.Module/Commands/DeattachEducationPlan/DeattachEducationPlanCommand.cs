using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Application.Shared.Users.Module.Commands.Common;
using SPerfomance.Application.StudentGroups.Module.Repository;
using SPerfomance.Application.StudentGroups.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.StudentGroups.Module.Commands.DeattachEducationPlan;

internal sealed class DeattachEducationPlanCommand : ICommand
{
	private readonly IRepositoryExpression<StudentGroup> _getGroup;
	private readonly StudentGroupQueryRepository _repository = new StudentGroupQueryRepository();

	public readonly ICommandHandler<DeattachEducationPlanCommand, StudentGroup> Handler;

	public DeattachEducationPlanCommand(StudentsGroupSchema group, string token)
	{
		_getGroup = ExpressionsFactory.GetByName(group.ToRepositoryObject());
		_repository = new StudentGroupQueryRepository();
		Handler = new VerificationHandler<DeattachEducationPlanCommand, StudentGroup>(token, User.Admin);
		Handler = new CommandHandler(Handler, _repository);
	}

	internal sealed class CommandHandler : DecoratedCommandHandler<DeattachEducationPlanCommand, StudentGroup>
	{
		private readonly StudentGroupQueryRepository _repository;

		public CommandHandler(
			ICommandHandler<DeattachEducationPlanCommand, StudentGroup> handler,
			StudentGroupQueryRepository repository)
		 : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<StudentGroup>> Handle(DeattachEducationPlanCommand command)
		{
			var result = await base.Handle(command);
			if (result.IsFailed)
				return result;

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
