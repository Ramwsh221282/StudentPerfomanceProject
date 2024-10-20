using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Application.Shared.Users.Module.Commands.Common;
using SPerfomance.Application.StudentGroups.Module.Repository;
using SPerfomance.Application.StudentGroups.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans.Errors;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.StudentGroups.Module.Commands.AttachEducationPlan;

internal sealed class AttachEducationPlanCommand : ICommand
{
	private readonly IRepositoryExpression<EducationPlan> _getPlan;
	private readonly IRepositoryExpression<StudentGroup> _getGroup;
	private readonly StudentGroupQueryRepository _repository;

	public readonly ICommandHandler<AttachEducationPlanCommand, StudentGroup> Handler;

	public AttachEducationPlanCommand(
		StudentsGroupSchema group,
		EducationPlanSchema plan,
		string token)
	{
		_getGroup = ExpressionsFactory.GetByName(group.ToRepositoryObject());
		_getPlan = ExpressionsFactory.GetPlan(plan.ToRepositoryObject());
		_repository = new StudentGroupQueryRepository();
		Handler = new VerificationHandler<AttachEducationPlanCommand, StudentGroup>(token, User.Admin);
		Handler = new DefaultCommandHandler(Handler, _repository);
		Handler = new AttachEducationPlanHandler(Handler, _repository);
		Handler = new CommitHandler(Handler, _repository);
	}

	internal sealed class DefaultCommandHandler : DecoratedCommandHandler<AttachEducationPlanCommand, StudentGroup>
	{
		private readonly StudentGroupQueryRepository _repository;

		public DefaultCommandHandler(
			ICommandHandler<AttachEducationPlanCommand, StudentGroup> handler,
			StudentGroupQueryRepository repository
			) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<StudentGroup>> Handle(AttachEducationPlanCommand command)
		{
			var result = await base.Handle(command);
			if (result.IsFailed)
				return result;

			StudentGroup? group = await _repository.GetByParameter(command._getGroup);
			return group == null ?
				new OperationResult<StudentGroup>(new GroupNotFoundError().ToString()) :
				new OperationResult<StudentGroup>(group);
		}
	}

	internal sealed class AttachEducationPlanHandler : DecoratedCommandHandler<AttachEducationPlanCommand, StudentGroup>
	{
		private readonly StudentGroupQueryRepository _repository;

		public AttachEducationPlanHandler(
			ICommandHandler<AttachEducationPlanCommand, StudentGroup> handler,
			StudentGroupQueryRepository repository)
		: base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<StudentGroup>> Handle(AttachEducationPlanCommand command)
		{
			OperationResult<StudentGroup> result = await base.Handle(command);
			if (result.Result == null || result.IsFailed)
				return result;

			EducationPlan? plan = await _repository.GetByParameter(command._getPlan);
			if (plan == null)
				return new OperationResult<StudentGroup>(new EducationPlanNotFoundError().ToString());

			Result attach = plan.RegisterGroup(result.Result);
			return attach.IsFailure ?
				new OperationResult<StudentGroup>(attach.Error) :
				result;
		}
	}

	internal sealed class CommitHandler : DecoratedCommandHandler<AttachEducationPlanCommand, StudentGroup>
	{
		private readonly StudentGroupQueryRepository _repository;

		public CommitHandler(
			ICommandHandler<AttachEducationPlanCommand, StudentGroup> handler,
			StudentGroupQueryRepository repository)
		: base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<StudentGroup>> Handle(AttachEducationPlanCommand command)
		{
			OperationResult<StudentGroup> result = await base.Handle(command);
			if (result.Result == null || result.IsFailed)
				return result;

			await _repository.Commit();
			return result;
		}
	}
}
